using Microsoft.Xna.Framework;
using Roguelike.Actors.UI;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors.AI;

public class Enemy : Actor, IDamageable, IActorCreatable<Enemy>
{
    public const string EnemyTag = "Enemy";

    private ColliderComponent colliderComponent;

    private HealthComponent healthComponent;

    private bool see;

    private Slider slider;

    private SpriteComponent spriteComponent;


    private int t;

    public Enemy(BaseGame game) : base(game)
    {
    }

    public override string Tag => EnemyTag;

    public static Enemy Create(BaseGame game)
    {
        return new(game);
    }

    public void TakeDamage(float damage)
    {
        healthComponent.Health -= damage;
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

        slider = Game.World.CreateActor<Slider>(Transform.ScreenPosition);
        slider.Ratio = 1;
        slider.Transform.Parent = Transform;
    }

    public override void Update(GameTime time)
    {
        base.Update(time);

        t++;

        if (!see && Game.World.Paths.DoesSee(Transform.Position, Hero.Instance.Transform.Position)) see = true;

        if (t > 50 && see)
        {
            t = 0;
            var nextCell = Game.World.Paths.NextCell(Transform.Position, Hero.Instance.Transform.Position);
            Transform.Position = new Vector2Int(nextCell.X, nextCell.Y);
        }
    }

    private void OnDeath()
    {
        Dispose();
    }

    private void OnChangeHealth()
    {
        slider.Ratio = healthComponent.HealthRatio;
    }
}