using System;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

public class Arrow : Actor, IActorCreatable<Arrow>, ICloneable
{
    private ColliderComponent collider;
    private SpriteComponent spriteComponent;
    private DamagerComponent damagerComponent;

    public Arrow(BaseGame game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        World.onPlayerMove += Move;

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Arrow");

        damagerComponent = AddComponent<DamagerComponent>();
        damagerComponent.Damages = new() { { Vector2Int.Zero, Damage } };
        World.onPlayerMove += damagerComponent.Damage;

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
        collider.OnTriggerEnter += OnTriggerEnter;
    }

    public override string Tag => Tags.ArrowTag;

    public bool IsMoving { get; set; } = true;

    private int damage;

    public int Damage
    {
        get => damage;
        set
        {
            damage = value;
            damagerComponent.Damages[Vector2Int.Zero] = damage;
        }
    }

    public static Arrow Create(BaseGame game)
    {
        return new Arrow(game);
    }

    public object Clone()
    {
        var clone = Game.World.CreateActor<Arrow>();
        clone.Damage = Damage;
        return clone;
    }

    public static Arrow GetPrototype(BaseGame game, int damage)
    {
        var prototype = game.World.CreateActor<Arrow>();
        prototype.Transform.Position = new Vector2Int(-1, -1);
        prototype.IsMoving = false;
        prototype.Damage = damage;
        return prototype;
    }

    public void Move()
    {
        if (!IsMoving) return;
        Transform.Position += Transform.Direction;
    }

    protected override void Dispose(bool isDisposing)
    {
        World.onPlayerMove -= damagerComponent.Damage;
        base.Dispose(isDisposing);
    }

    public void OnTriggerEnter(ColliderComponent other)
    {
        if (other.Type == ColliderType.Solid) Dispose();
    }
}