﻿using Roguelike.Actors.Enemies.AI.Behaivour;
using Roguelike.Actors.UI;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

public abstract class Enemy : Actor, IDamageable
{
    protected EnemyBehaviour Behaviour;
    protected ColliderComponent ColliderComponent;
    protected HealthComponent HealthComponent;
    protected DamagerComponent DamagerComponent;

    protected Slider HealthSlider;

    protected SpriteComponent SpriteComponent;

    protected Enemy(BaseGame game) : base(game)
    {
    }

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
        World.onPlayerMove += DamagerComponent.Damage;

        HealthSlider = Game.World.CreateActor<Slider>(Transform.ScreenPosition);
        HealthSlider.Ratio = 1;
        HealthSlider.Transform.Parent = Transform;

        World.onPlayerMove += RunBehaviour;
    }

    public void RunBehaviour()
    {
        Behaviour.Run();
    }

    private void OnDeath()
    {
        World.onPlayerMove -= RunBehaviour;
        World.onPlayerMove -= DamagerComponent.Damage;
        Dispose();
    }

    private void OnChangeHealth()
    {
        HealthSlider.Ratio = HealthComponent.HealthRatio;
    }

    public override string Tag => Tags.EnemyTag;

    public abstract void TakeDamage(int damage);
}