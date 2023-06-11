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
        SpriteComponent.SetTexture("Frog");
        Behaviour = new AggressiveBehaviour(this);
        DamagerComponent.Damages = new Dictionary<Vector2Int, int> { { Vector2Int.Up, 5 }, { Vector2Int.Zero, 10 } };
    }

    public override void TakeDamage(int damage)
    {
        HealthComponent.Health -= damage;
        Behaviour.IsAttacked = true;
    }
}