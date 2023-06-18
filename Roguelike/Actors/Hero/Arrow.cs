using System;
using Microsoft.Xna.Framework;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

public class Arrow : Actor, IMovable, IActorCreatable<Arrow>
{
    private ColliderComponent collider;
    private SpriteComponent spriteComponent;

    public Arrow(BaseGame game) : base(game)
    {
    }

    public float Damage { get; set; } = 0;

    public static Arrow Create(BaseGame game)
    {
        return new(game);
    }

    public void Move()
    {
        Vector2Int direction = new Vector2(MathF.Cos(Transform.Angle), MathF.Sin(Transform.Angle));
        Transform.Position += direction * 2;
    }

    public override void Initialize(Vector2Int position)
    {
        base.Initialize(position);

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Arrow");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
        collider.OnTriggerEnter += OnTriggerEnter;
    }

    public void OnTriggerEnter(ColliderComponent other)
    {
        if (other.Type == ColliderType.Solid)
        {
            Dispose();
            return;
        }

        if (other.Owner is IDamageable damageable)
        {
            damageable.TakeDamage(Damage);
            Dispose();
        }
    }
}