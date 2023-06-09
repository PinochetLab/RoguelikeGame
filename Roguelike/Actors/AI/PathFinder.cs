using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collections;
using Roguelike.Core;
using Roguelike.Field;


namespace Roguelike.Actors.AI;

public class Cell
{
    public int X, Y;
    public bool IsWall;
    public bool Visited;
    public int DistanceFromStart;
    public int DistanceToEnd;
    public Cell Parent;

    public Cell(int x, int y, bool isWall)
    {
        X = x;
        Y = y;
        IsWall = isWall;
        Visited = false;
        DistanceFromStart = int.MaxValue;
        DistanceToEnd = int.MaxValue;
    }

    public void Reset()
    {
        Visited = false;
        DistanceFromStart = int.MaxValue;
        DistanceToEnd = int.MaxValue;
        Parent = null;
    }

    public Vector2Int V2 => new Vector2Int(X, Y);

    public int Distance => DistanceFromStart + DistanceToEnd;
}

public class PathFinder : BaseGameSystem
{
    private Cell[,] grid;
    private int width = 0;
    private int height = 0;

    public PathFinder(BaseGame game) : base(game)
    {
        
    }

    public override void Initialize()
    {
        base.Initialize();

        width = FieldInfo.CellCount;
        height = FieldInfo.CellCount;

        grid = new Cell[width, height];

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                grid[i, j] = new Cell(i, j, Game.World.Colliders.ContainsSolid(new Vector2Int(i, j)));
            }
        }
    }

    public void GeneratePath(Vector2Int s, Vector2Int e)
    {
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                grid[i, j].Reset();
            }
        }

        var start = grid[s.X, s.Y];
        var end = grid[e.X, e.Y];
        start.DistanceFromStart = 0;
        start.DistanceToEnd = Heuristic(start, end);

        var openList = new Queue<Cell>();
        openList.Enqueue(start);

        while (openList.Count > 0)
        {
            var current = openList.Dequeue();

            if (current == end)
            {
                break;
            }

            current.Visited = true;

            foreach (var neighbor in GetNeighbors(current))
            {
                if (neighbor.Visited || neighbor.IsWall) continue;

                var tentativeDistanceFromStart = current.DistanceFromStart + 1;
                if (tentativeDistanceFromStart < neighbor.DistanceFromStart)
                {
                    neighbor.DistanceFromStart = tentativeDistanceFromStart;
                    neighbor.DistanceToEnd = Heuristic(neighbor, end);
                    neighbor.Visited = true;
                    neighbor.Parent = current;
                    if (!openList.Contains(neighbor))
                    {
                        openList.Enqueue(neighbor);
                    }
                }
            }
        }
    }

    public List<Vector2Int> GetPath(Vector2Int start, Vector2Int end)
    {
        GeneratePath(start, end);
        var path = new List<Vector2Int>();
        var current = grid[end.X, end.Y];
        
        while (current.Parent != null)
        {
            path.Add(current.V2);
            current = current.Parent;
        }
        path.Reverse();
        return path;
    }

    public Vector2Int NextCell(Vector2Int start, Vector2Int end)
    {
        var path = GetPath(start, end);
        if (path.Count > 0)
        {
            return path[0];
        }
        return start;
    }

    private int Heuristic(Cell c1, Cell c2)
    {
        return (int)(MathF.Abs(c1.X - c2.X) + MathF.Abs(c1.Y - c2.Y));
    }

    private List<Cell> GetNeighbors(Cell current)
    {
        var neighbors = new List<Cell>();
        if (current.X > 0)
            neighbors.Add(grid[current.X - 1, current.Y]);
        if (current.X < width - 1)
            neighbors.Add(grid[current.X + 1, current.Y]);
        if (current.Y > 0)
            neighbors.Add(grid[current.X, current.Y - 1]);
        if (current.Y < height - 1)
            neighbors.Add(grid[current.X, current.Y + 1]);
        return neighbors.Shuffle(new Random()).ToList();
    }

    public bool DoesSee(Vector2Int start, Vector2Int end)
    {
        Vector2 s = start;
        Vector2 e = end;
        int acc = 100;
        for (var i = 1; i < acc; i++)
        {
            var t = (float)i / acc;
            Vector2Int v = e * t + s * (1 - t);
            if (grid[v.X, v.Y].IsWall)
            {
                return false;
            }
        }
        return true;
    }
}
