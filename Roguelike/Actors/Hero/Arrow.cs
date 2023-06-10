using System;
using Microsoft.Xna.Framework;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

public class Arrow : Actor, IActorCreatable<Arrow>, ICloneable
{
    public override string Tag => "Arrow";
    private ColliderComponent collider;
    private SpriteComponent spriteComponent;

    public Arrow(BaseGame game) : base(game)
    {
    }

    public int Damage { get; set; }

    public static Arrow Create(BaseGame game)
    {
        return new(game);
    }

    public object Clone()
    {
        var clone = Create(Game);
        clone.Initialize(Transform.Position);
        clone.Damage = Damage;
        return clone;
    }

    public override void Initialize(Vector2Int position)
    {
        base.Initialize(position);
        World.onPlayerMove += Move;

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Arrow");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
        collider.OnTriggerEnter += OnTriggerEnter;
    }

    public void Move()
    {
        Transform.Position += Transform.Direction;
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