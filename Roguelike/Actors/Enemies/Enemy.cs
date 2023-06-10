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
    protected HealthComponent healthComponent;

    protected Slider healthSlider;

    protected SpriteComponent spriteComponent;

    protected Enemy(BaseGame game) : base(game)
    {
    }

    public override string Tag => Tags.EnemyTag;

    public abstract void TakeDamage(int damage);
}