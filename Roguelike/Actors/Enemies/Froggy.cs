using Roguelike.Actors.Enemies.AI;
using Roguelike.Actors.UI;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

public class Froggy : Enemy, IActorCreatable<Froggy>
{
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

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Frog");

        healthComponent = AddComponent<HealthComponent>();
        healthComponent.OnDeath += OnDeath;
        healthComponent.OnHealthChange += OnChangeHealth;

        colliderComponent = AddComponent<ColliderComponent>();
        colliderComponent.Type = ColliderType.Trigger;

        healthSlider = Game.World.CreateActor<Slider>(Transform.ScreenPosition);
        healthSlider.Ratio = 1;
        healthSlider.Transform.Parent = Transform;

        behaviour = new AgressiveBehaviour(this);
        World.onPlayerMove += () => behaviour.Run();
    }

    public override void TakeDamage(float damage)
    {
        healthComponent.Health -= damage;
    }

    private void OnDeath()
    {
        Destroy();
    }

    private void OnChangeHealth()
    {
        healthSlider.Ratio = healthComponent.HealthRatio;
    }
}