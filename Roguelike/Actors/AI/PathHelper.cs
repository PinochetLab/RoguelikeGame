using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.Components.Colliders;
using Roguelike.Field;
using Roguelike.VectorUtility;
using Microsoft.Xna.Framework;

namespace Roguelike.Actors.AI;
public static class PathHelper
{
    private static int _iteration = 0;

    public static int[][] Visited = Enumerable.Repeat(Enumerable.Repeat(0, FieldInfo.CellCount).ToArray(), FieldInfo.CellCount).ToArray();

    public static Vector2Int[][] NextCells = Enumerable.Repeat(Enumerable.Repeat(Vector2Int.Zero, FieldInfo.CellCount).ToArray(), FieldInfo.CellCount).ToArray();
    
    public static Vector2Int NextCell(Vector2Int start, Vector2Int end)
    {
        Queue<Vector2Int> q = new Queue<Vector2Int>();

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

            if (ColliderManager.ContainsSolid(s)) continue;

            if (Visited[s.X][s.Y] == _iteration) continue;

            Visited[s.X][s.Y] = _iteration;

            var ns = new List<Vector2Int>() { s + Vector2Int.Right, s + Vector2Int.Left, s + Vector2Int.Up, s + Vector2Int.Down };

            foreach (var n in ns)
            {
                if (!first)
                {
                    NextCells[n.X][n.Y] = NextCells[s.X][s.Y];
                }
                else
                {
                    NextCells[n.X][n.Y] = n;
                }
                q.Enqueue(n);
            }

            first = false;
        }
        return start;
    }


    public static bool CanSeeTarget(Vector2Int v, Vector2Int t, float distanceOfView)
    {
        if (v == t) return true;
        if (Vector2Int.Distance(v, t) > distanceOfView) return false;

        var r = v;
        var d = (Vector2)(t - v) / Vector2.Distance(t, v);

        for (var i = 0; i < FieldInfo.CellCount; i++)
            for (var j = 0; j < FieldInfo.CellCount; j++)
            {
                Vector2 p = new Vector2Int(i, j);
                if (ColliderManager.ContainsSolid(p))
                {
                    int maxK = 30;
                    for (int k = 0; k < maxK; k++)
                    {
                        Vector2 c = Vector2.Lerp(v, t, (float)k / maxK);
                        if (c.X > p.X - 0.5f && c.X < p.X + 0.5f && c.Y > p.Y - 0.5f && c.Y < 0.5f)
                        {
                            return false;
                        }
                    }
                }
            }
        return true;
    }
}
