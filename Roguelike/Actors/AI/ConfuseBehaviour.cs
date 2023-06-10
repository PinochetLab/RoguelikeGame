using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Roguelike.Components.Colliders;
using Roguelike.Core;

namespace Roguelike.Actors.AI;
public class ConfuseBehaviour : Behaviour
{
    public override void Run()
    {
        var p = Owner.Transform.Position;
        var neighbours = new List<Vector2Int>()
        {
            p + Vector2Int.Right,
            p + Vector2Int.Left,
            p + Vector2Int.Up,
            p + Vector2Int.Down,
        }.Where(x => !Owner.Game.World.Colliders.ContainsSolid(x)).ToList();
        Owner.Transform.Position = neighbours[new Random().Next(neighbours.Count)];
    }
}
