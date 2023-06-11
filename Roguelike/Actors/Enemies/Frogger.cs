using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Roguelike.Actors.Enemies.AI;
using Roguelike.Actors.Enemies.AI.Behaivour;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

public class Frogger : Enemy, IActorCreatable<Frogger>
{
    public Frogger(BaseGame game) : base(game)
    {
    }

    public static Frogger Create(BaseGame game)
    {
        return new Frogger(game);
    }

    public override void Initialize()
    {
        base.Initialize();
        spriteComponent.SetTexture("Frog");
        spriteComponent.Color = Color.Yellow;
        healthComponent.SetMaxHealth(20);
        behaviour = new LazyBehaviour(this);
        damagerComponent.Damages = new Dictionary<Vector2Int, int> { { Vector2Int.Up, 10 }, { Vector2Int.Zero, 15 } };
    }

    public override void TakeDamage(int damage)
    {
        healthComponent.Health -= damage;
        behaviour.IsAttacked = true;
    }
}