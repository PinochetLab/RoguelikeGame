using System;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

public class Arrow : Actor, IActorCreatable<Arrow>, ICloneable
{
    private ColliderComponent collider;
    private SpriteComponent spriteComponent;

    public Arrow(BaseGame game) : base(game)
    {
    }

    public override string Tag => "Arrow";

    public bool IsMoving { get; set; } = true;
    public int Damage { get; set; }

    public static Arrow Create(BaseGame game)
    {
        return new(game);
    }

    public object Clone()
    {
        var clone = Game.World.CreateActor<Arrow>();
        clone.Damage = Damage;
        return clone;
    }

    public static Arrow GetPrototype(BaseGame game, float damage)
    {
        var prototype = game.World.CreateActor<Arrow>();
        prototype.Transform.Position = new Vector2Int(-1, -1);
        prototype.IsMoving = false;
        prototype.Damage = damage;
        return prototype;
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
        if (!IsMoving) return;
        Transform.Position += Transform.Direction;
    }

    public void OnTriggerEnter(ColliderComponent other)
    {
        if (other.Owner is IDamageable damageable) damageable.TakeDamage(Damage);

        if (other.Type == ColliderType.Solid) Dispose();
    }
}