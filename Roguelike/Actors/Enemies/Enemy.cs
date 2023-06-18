using Roguelike.Actors.Enemies.AI;
using Roguelike.Actors.UI;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

public abstract class Enemy : Actor, IDamageable
{
    protected EnemyBehaviour behaviour;
    protected ColliderComponent colliderComponent;
    protected DamagerComponent damagerComponent;
    protected HealthComponent healthComponent;

    protected Slider healthSlider;

    protected SpriteComponent spriteComponent;

    protected Enemy(BaseGame game) : base(game)
    {
    }

    public override string Tag => Tags.EnemyTag;

    public abstract void TakeDamage(int damage);

    public override void Initialize()
    {
        base.Initialize();

        spriteComponent = AddComponent<SpriteComponent>();

        colliderComponent = AddComponent<ColliderComponent>();
        colliderComponent.Type = ColliderType.Trigger;

        healthComponent = AddComponent<HealthComponent>();
        healthComponent.OnDeath += OnDeath;
        healthComponent.OnHealthChange += OnChangeHealth;

        damagerComponent = AddComponent<DamagerComponent>();
        World.onPlayerMove += damagerComponent.Damage;

        healthSlider = Game.World.CreateActor<Slider>(Transform.ScreenPosition);
        healthSlider.Ratio = 1;
        healthSlider.Transform.Parent = Transform;

        World.onPlayerMove += RunBehaviour;
    }

    public void RunBehaviour()
    {
        behaviour.Run();
    }

    private void OnDeath()
    {
        World.onPlayerMove -= RunBehaviour;
        World.onPlayerMove -= damagerComponent.Damage;
        Dispose();
    }

    private void OnChangeHealth()
    {
        healthSlider.Ratio = healthComponent.HealthRatio;
    }
}