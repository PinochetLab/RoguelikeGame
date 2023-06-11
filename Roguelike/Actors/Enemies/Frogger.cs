using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Roguelike.Actors.Enemies.AI.Behaivour;
using Roguelike.Actors.Enemies.AI.StateMachine;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

public class Frogger : Enemy, IActorCreatable<Frogger>
{
    public Frogger(BaseGame game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        SpriteComponent.SetTexture("Frog");
        SpriteComponent.Color = Color.Yellow;
        HealthComponent.SetMaxHealth(20, true);
        DamagerComponent.Damages = new Dictionary<Vector2Int, int> { { Vector2Int.Up, 10 }, { Vector2Int.Zero, 15 } };
    }

    public override StateMachine<EnemyBehaviour> InitializeBehaviour()
    {
        var machine = new StateMachine<EnemyBehaviour>(nameof(Froggy));

        var behaviour = new LazyBehaviour(this);
        machine.Add(behaviour)
            .Calls(x => x.Behaviour.State.Run());

        machine.CurrentState = behaviour;
        return machine;
    }

    public override void TakeDamage(int damage)
    {
        HealthComponent.Health -= damage;
        BehaviourStates.CurrentState.IsAttacked = true;
    }

    public static Frogger Create(BaseGame game) => new(game);
}