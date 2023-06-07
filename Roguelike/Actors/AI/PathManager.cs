using System.Collections.Generic;
using System.Linq;
using Roguelike.Field;
using Microsoft.Xna.Framework;
using Roguelike.Core;

namespace Roguelike.Actors.AI;
public class PathManager : BaseGameSystem
{
    private int _iteration = 0;

    private int[][] Visited { get; } = Enumerable.Repeat(
            Enumerable.Repeat(0, FieldInfo.CellCount).ToArray(),
            FieldInfo.CellCount)
        .ToArray();

    private Vector2Int[][] NextCells { get; } = Enumerable.Repeat(
            Enumerable.Repeat(Vector2Int.Zero, FieldInfo.CellCount).ToArray(),
            FieldInfo.CellCount)
        .ToArray();

    public PathManager(BaseGame game) : base(game)
    { }

    public Vector2Int NextCell(Vector2Int start, Vector2Int end)
    {
        var q = new Queue<Vector2Int>();

        _iteration++;

        q.Enqueue(start);
        var first = true;

        while (q.Count > 0)
        {
            var s = q.Dequeue();

            if (s == end)
            {
                return NextCells[end.X][end.Y];
            }

            if (Game.World.Colliders.ContainsSolid(s)) continue;

            if (Visited[s.X][s.Y] == _iteration) continue;

            Visited[s.X][s.Y] = _iteration;

            foreach (var n in GetNext(s))
            {
                NextCells[n.X][n.Y] = !first ? NextCells[s.X][s.Y] : n;
                q.Enqueue(n);
            }

            first = false;
        }
        return start;
    }


    public bool CanSeeTarget(Vector2Int v, Vector2Int t, float distanceOfView)
    {
        if (v == t) return true;
        if (Vector2Int.Distance(v, t) > distanceOfView) return false;

        for (var i = 0; i < FieldInfo.CellCount; i++)
        for (var j = 0; j < FieldInfo.CellCount; j++)
        {
            Vector2 p = new Vector2Int(i, j);
            if (!Game.World.Colliders.ContainsSolid(p)) continue;

            const int maxK = 30;
            for (var k = 0; k < maxK; k++)
            {
                var c = Vector2.Lerp(v, t, (float)k / maxK);
                if (c.X > p.X - 0.5f && c.X < p.X + 0.5f && c.Y > p.Y - 0.5f && c.Y < 0.5f)
                    return false;
            }
        }
        return true;
    }

    private static IEnumerable<Vector2Int> GetNext(Vector2Int current)
    {
        yield return current + Vector2Int.Right;
        yield return current + Vector2Int.Left;
        yield return current + Vector2Int.Up;
        yield return current + Vector2Int.Down;
    }
}
