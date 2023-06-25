using System.Collections.Generic;
using MonoGame.Extended;
using Roguelike.Actors.Enemies.AI.Behaviour;
using Roguelike.Actors.Enemies.AI.StateMachine;
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
        HealthComponent.SetMaxHealth(40, true);
        DamagerComponent.Damages = new Dictionary<Vector2Int, int> { { Vector2Int.Up, 5 }, { Vector2Int.Zero, 10 } };
        expInside = 100;
    }

    public override StateMachine<EnemyBehaviour> InitializeBehaviour()
    {
        var machine = new StateMachine<EnemyBehaviour>(nameof(Froggy));

        var defaultBehaviour = new AggressiveBehaviour(this);
        var cowardlyBehaviour = new CowardlyBehaviour(this);
        var confusedBehaviour = new ConfusedBehaviour(this);

        var random = new FastRandom();


        machine.Add(defaultBehaviour)
            .GoesTo(cowardlyBehaviour)
            .Calls(x =>
            {
                if (HealthComponent.HealthRatio < 0.5)
                    x.Next();
                x.State.Run();
            });

        machine.Add(cowardlyBehaviour)
            .GoesTo(defaultBehaviour)
            .OnEnter(() => { cowardlyBehaviour.GenerateHidingSpot(); })
            .Calls(x =>
            {
                if (HealthComponent.HealthRatio >= 0.5)
                    x.Next();
                x.State.Run();
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