using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collections;
using Roguelike.Core;
using Roguelike.Field;

namespace Roguelike.Actors.Enemies.AI.Behaviour;

public class PathFinder : BaseGameSystem
{
    private Cell[,] grid;
    public int WorldHeight { get; private set; }
    public int WorldWidth { get; private set; }

    public PathFinder(BaseGame game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        WorldWidth = FieldInfo.CellCount;
        WorldHeight = FieldInfo.CellCount;

        grid = new Cell[WorldWidth, WorldHeight];

        for (var i = 0; i < WorldWidth; i++)
        for (var j = 0; j < WorldHeight; j++)
            grid[i, j] = new Cell(i, j, Game.World.Colliders.ContainsSolid(new Vector2Int(i, j)));
    }

    public void GeneratePath(Vector2Int s, Vector2Int e)
    {
        for (var i = 0; i < WorldWidth; i++)
        for (var j = 0; j < WorldHeight; j++)
            grid[i, j].Reset();

        var start = grid[s.X, s.Y];
        var end = grid[e.X, e.Y];
        start.DistanceFromStart = 0;
        start.DistanceToEnd = Heuristic(start, end);

        var openList = new Queue<Cell>();
        openList.Enqueue(start);

        while (openList.Count > 0)
        {
            var current = openList.Dequeue();

            if (current == end) break;

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
                    if (!openList.Contains(neighbor)) openList.Enqueue(neighbor);
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
        if (path.Count > 0) return path[0];
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
        if (current.X < WorldWidth - 1)
            neighbors.Add(grid[current.X + 1, current.Y]);
        if (current.Y > 0)
            neighbors.Add(grid[current.X, current.Y - 1]);
        if (current.Y < WorldHeight - 1)
            neighbors.Add(grid[current.X, current.Y + 1]);
        return neighbors.Shuffle(new Random()).ToList();
    }

    public bool DoesSee(Vector2Int start, Vector2Int end)
    {
        var s = (Vector2)start + Vector2.One * 0.5f;
        Vector2 e = end;
        var acc = 100;
        for (var i = 1; i < acc; i++)
        {
            var t = (float)i / acc;
            Vector2Int v = e * t + s * (1 - t);
            if (grid[v.X, v.Y].IsWall) return false;
        }

        return true;
    }

    public record Cell(int X, int Y, bool IsWall)
    {
        public int DistanceFromStart { get; set; } = int.MaxValue;
        public int DistanceToEnd { get; set; } = int.MaxValue;
        public bool IsWall { get; } = IsWall;
        public Cell Parent { get; set; }
        public bool Visited { get; set; }
        public int X { get; } = X;
        public int Y { get; } = Y;

        public Vector2Int V2 => new(X, Y);

        public int Distance => DistanceFromStart + DistanceToEnd;

        public void Reset()
        {
            Visited = false;
            DistanceFromStart = int.MaxValue;
            DistanceToEnd = int.MaxValue;
            Parent = null;
        }
    }
}