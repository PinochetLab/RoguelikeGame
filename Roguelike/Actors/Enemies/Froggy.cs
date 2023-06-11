using System.Collections.Generic;
using Roguelike.Actors.Enemies.AI;
using Roguelike.Actors.Enemies.AI.Behaivour;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

public class Froggy : Enemy, IActorCreatable<Froggy>
{
    public Froggy(BaseGame game) : base(game)
    {
    }

    public static Froggy Create(BaseGame game)
    {
        return new Froggy(game);
    }

    public override void Initialize()
    {
        base.Initialize();
        spriteComponent.SetTexture("Frog");
        behaviour = new AggressiveBehaviour(this);
        damagerComponent.Damages = new Dictionary<Vector2Int, int> { { Vector2Int.Up, 5 }, { Vector2Int.Zero, 10 } };
    }

    public override void TakeDamage(int damage)
    {
        healthComponent.Health -= damage;
        behaviour.IsAttacked = true;
    }
}