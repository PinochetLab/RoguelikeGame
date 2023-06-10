using Roguelike.Actors.Enemies.AI;
using Roguelike.Actors.UI;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors.Enemies;

public abstract class Enemy : Actor, IDamageable
{
    public override string Tag => Tags.EnemyTag;

    protected SpriteComponent spriteComponent;
    protected HealthComponent healthComponent;
    protected ColliderComponent colliderComponent;
    
    protected Slider healthSlider;

    protected EnemyBehaviour behaviour;

    protected Enemy(BaseGame game) : base(game)
    {
    }

    public abstract void TakeDamage(float damage);
}
