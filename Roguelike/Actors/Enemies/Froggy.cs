using System.Collections.Generic;
using Roguelike.Actors.Enemies.AI.Behaivour;
using Roguelike.Actors.Enemies.AI.StateMachine;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

public class Froggy : Enemy, IActorCreatable<Froggy>
{
    private LazyBehaviour cowardBehaviour;
    private AggressiveBehaviour defaultBehaviour;

    public Froggy(BaseGame game) : base(game)
    {
    }

    public static Froggy Create(BaseGame game)
    {
        return new(game);
    }

    public override void Initialize()
    {
        base.Initialize();
        SpriteComponent.SetTexture("Frog");
        HealthComponent.SetMaxHealth(40, true);
        DamagerComponent.Damages = new Dictionary<Vector2Int, int> { { Vector2Int.Up, 5 }, { Vector2Int.Zero, 10 } };
    }

    public override StateMachine<EnemyBehaviour> InitializeBehaviour()
    {
        var machine = new StateMachine<EnemyBehaviour>(nameof(Froggy));

        defaultBehaviour = new AggressiveBehaviour(this);
        cowardBehaviour = new LazyBehaviour(this);
        machine.Add(defaultBehaviour)
            .GoesTo(cowardBehaviour)
            .Calls(x =>
            {
                if (HealthComponent.HealthRatio < 0.5)
                    x.Next();
                x.Behaviour.State.Run();
            });

        machine.Add(cowardBehaviour)
            .GoesTo(defaultBehaviour)
            .Calls(x =>
            {
                if (HealthComponent.HealthRatio >= 0.5)
                    x.Next();
                x.Behaviour.State.Run();
                HealthComponent.Health += 5;
            });

        machine.CurrentState = defaultBehaviour;
        return machine;
    }

    public override void TakeDamage(int damage)
    {
        HealthComponent.Health -= damage;
        BehaviourStates.CurrentState.IsAttacked = true;
    }
}