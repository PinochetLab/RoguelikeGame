using System.Collections.Generic;
using Roguelike.Actors.Enemies.AI;
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
        behaviour = new AgressiveBehaviour(this);
        damagerComponent.Damages = new Dictionary<Vector2Int, int> { { Vector2Int.Up, 5 }, { Vector2Int.Zero, 10 } };
        expInside = 100;
    }

    public override void TakeDamage(int damage)
    {
        healthComponent.Health -= damage;
        behaviour.IsAttacked = true;
    }
}