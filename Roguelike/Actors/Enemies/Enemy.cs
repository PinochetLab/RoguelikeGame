using Roguelike.Actors.Enemies.AI.Behaviour;
using Roguelike.Actors.Enemies.AI.StateMachine;
using Roguelike.Actors.UI;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

/// <summary>
///     Данный класс отвечает абстрактного врага
/// </summary>
public abstract class Enemy : Actor, IDamageable
{
    protected StateMachine<EnemyBehaviour> BehaviourStates;
    protected ColliderComponent ColliderComponent;
    protected DamagerComponent DamagerComponent;

    protected int expInside;
    protected HealthComponent HealthComponent;

    protected Slider HealthSlider;

    protected SpriteComponent SpriteComponent;

    protected Enemy(BaseGame game) : base(game)
    {
    }

    public override string Tag => Tags.EnemyTag;

    public abstract void TakeDamage(int damage);

    public override void Initialize()
    {
        base.Initialize();

        SpriteComponent = AddComponent<SpriteComponent>();

        ColliderComponent = AddComponent<ColliderComponent>();
        ColliderComponent.Type = ColliderType.Trigger;

        HealthComponent = AddComponent<HealthComponent>();
        HealthComponent.OnDeath += OnDeath;
        HealthComponent.OnHealthChange += OnChangeHealth;

        DamagerComponent = AddComponent<DamagerComponent>();
        World.onHeroCommand += DamagerComponent.Damage;

        HealthSlider = Game.World.CreateActor<Slider>(Transform.ScreenPosition);
        HealthSlider.Ratio = 1;
        HealthSlider.Transform.Parent = Transform;

        World.onHeroCommand += RunBehaviour;

        BehaviourStates = InitializeBehaviour();
    }

    protected abstract StateMachine<EnemyBehaviour> InitializeBehaviour();

    private void RunBehaviour()
    {
        BehaviourStates.Process(1);
    }

    /// <summary>
    ///     Налагает на врага эффект конфузии
    /// </summary>
    public void Confuse()
    {
        var confusion = BehaviourStates.GetBehaviour<ConfusedBehaviour>(0);
        if (confusion == null)
            confusion = BehaviourStates.Add(new ConfusedBehaviour(this))
                .Calls(x =>
                {
                    if (x.State is not ConfusedBehaviour state) return;

                    state.Run();
                    if (state.TimeLeft == 0)
                    {
                        state.TimeLeft = ConfusedBehaviour.MaxTimeLeft;
                        x.Machine.CurrentState = state.PreviousBehaviour;
                    }
                });

        if (confusion.State is not ConfusedBehaviour behaviour) return;
        if (BehaviourStates.CurrentState is ConfusedBehaviour c)
        {
            c.TimeLeft = ConfusedBehaviour.MaxTimeLeft;
            return;
        }

        behaviour.PreviousBehaviour = BehaviourStates.CurrentState;
        BehaviourStates.CurrentState = behaviour;
    }

    private void OnDeath()
    {
        World.onHeroCommand -= RunBehaviour;
        World.onHeroCommand -= DamagerComponent.Damage;
        HealthComponent.OnDeath -= OnDeath;
        World.Stats.Exp += expInside;
        Dispose();
    }

    private void OnChangeHealth()
    {
        HealthSlider.Ratio = HealthComponent.HealthRatio;
    }
}