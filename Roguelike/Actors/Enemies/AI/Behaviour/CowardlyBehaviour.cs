using MonoGame.Extended;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies.AI.Behaviour;

public class CowardlyBehaviour : EnemyBehaviour
{
    private readonly FastRandom random = new();
    private Vector2Int offset;

    public CowardlyBehaviour(Actor actor) : base(actor)
    {
    }

    public void GenerateHidingSpot()
    {
        var world = Actor.World;
        do
        {
            offset = new Vector2Int(random.Next(world.Paths.WorldHeight), random.Next(world.Paths.WorldWidth));
        } while ((world.Colliders.ColliderMap.TryGetValue(offset, out var list) && list.Count != 0) ||
                 Vector2Int.Distance(offset, Hero.Instance.Transform.Position) < 3);
    }

    public override void Run()
    {
        Actor.Transform.Position =
            Actor.World.Paths.NextCell(Actor.Transform.Position, offset);
    }
}