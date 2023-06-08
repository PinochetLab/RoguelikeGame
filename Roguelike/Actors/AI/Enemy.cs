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
    private SpriteComponent spriteComponent;

    private HealthComponent healthComponent;

    private ColliderComponent colliderComponent;

    private Slider slider;

    public Enemy(BaseGame game) : base(game)
    {

    }

    public static Enemy Create(BaseGame game) => new Enemy(game);

    public override void OnStart()
    {
        base.OnStart();

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

    public override void Update(GameTime time)
    {
        base.Update(time);

        t++;
        if (t > 20)
        {
            t = 0;
            var cells = new List<Vector2Int>() { Transform.Position + Vector2Int.Right, Transform.Position + Vector2Int.Left,
                Transform.Position + Vector2Int.Up, Transform.Position + Vector2Int.Down};

            foreach (Vector2Int c in cells)
            {
                if (World.Colliders.ContainsSolid(c)) continue;
                Transform.Position = c;
                break;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        healthComponent.Health -= damage;
    }

    private void OnDeath()
    {
        Destroy();
    }

    private void OnChangeHealth()
    {
        slider.Ratio = healthComponent.HealthRatio;
    }
}
