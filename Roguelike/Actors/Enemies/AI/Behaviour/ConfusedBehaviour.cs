using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies.AI.Behaviour;
/// <summary>
///     Замешательство - движется в случайных направлениях MaxTimeLeft ходов
/// </summary>
public class ConfusedBehaviour : EnemyBehaviour
{
    public const int MaxTimeLeft = 4;

    public ConfusedBehaviour(Actor actor) : base(actor)
    {
    }

    public EnemyBehaviour PreviousBehaviour { get; set; }
    public int TimeLeft { get; set; } = MaxTimeLeft;

    public override void Run()
    {
        TimeLeft--;
        var neighbours = GetDirections().Where(x => !Actor.Game.World.Colliders.ContainsSolid(x)).ToList();
        Actor.Transform.Position = neighbours[Random.Shared.Next(neighbours.Count)];
    }

    private IEnumerable<Vector2Int> GetDirections()
    {
        var pos = Actor.Transform.Position;
        yield return pos + Vector2Int.Right;
        yield return pos + Vector2Int.Left;
        yield return pos + Vector2Int.Up;
        yield return pos + Vector2Int.Down;
    }
}