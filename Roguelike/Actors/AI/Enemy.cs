using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
    public override string Tag => EnemyTag;

    private SpriteComponent spriteComponent;

    private HealthComponent healthComponent;

    private ColliderComponent colliderComponent;

    private Slider slider;

    public Enemy(BaseGame game) : base(game)
    {

    }

    public static Enemy Create(BaseGame game) => new Enemy(game);

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


    private int t = 0;

    private bool see = false;

    public override void Update(GameTime time)
    {
        base.Update(time);

        t++;

        if (!see && Game.World.Paths.DoesSee(Transform.Position, Hero.Instance.Transform.Position))
        {
            see = true;
        }

        if (t > 50 && see)
        {
            t = 0;
            var nextCell = Game.World.Paths.NextCell(Transform.Position, Hero.Instance.Transform.Position);
            Transform.Position = new Vector2Int(nextCell.X, nextCell.Y);
        }
    }

    public void TakeDamage(float damage)
    {
        healthComponent.Health -= damage;
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
