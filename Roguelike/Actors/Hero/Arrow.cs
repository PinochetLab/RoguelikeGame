using System;
using Microsoft.Xna.Framework;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using IMovable = Roguelike.Components.IMovable;
using System.Diagnostics;
using Roguelike.Core;

namespace Roguelike.Actors;

public class Arrow : Actor, IMovable, IActorCreatable<Arrow>
{
    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public Arrow(BaseGame game) : base(game)
    { }

    public override void Initialize(Vector2Int position)
    {
        base.Initialize(position);

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Arrow");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
        collider.OnTriggerEnter += OnTriggerEnter;
    }

    public void Move()
    {
        Vector2Int direction = new Vector2(MathF.Cos(Transform.Angle), MathF.Sin(Transform.Angle));
        Debug.WriteLine(Transform.Position);
        Transform.Position += direction;
    }

    public void OnTriggerEnter(ColliderComponent other)
    {
        if (other.Type == ColliderType.Trigger) return;
        Destroy();
    }

    public static Arrow Create(BaseGame game) => new(game);
}
