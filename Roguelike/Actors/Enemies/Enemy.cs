using Roguelike.Actors.Enemies.AI.Behaivour;
using Roguelike.Actors.Enemies.AI.StateMachine;
using Roguelike.Actors.UI;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

public abstract class Enemy : Actor, IDamageable
{
    protected StateMachine<EnemyBehaviour> BehaviourStates;
    protected ColliderComponent ColliderComponent;
    protected DamagerComponent DamagerComponent;
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

    public abstract StateMachine<EnemyBehaviour> InitializeBehaviour();

    public void RunBehaviour()
    {
        BehaviourStates.Process(1);
    }

    private void OnDeath()
    {
        World.onHeroCommand -= RunBehaviour;
        World.onHeroCommand -= DamagerComponent.Damage;
        Dispose();
    }

    private void OnChangeHealth()
    {
        HealthSlider.Ratio = HealthComponent.HealthRatio;
    }
}