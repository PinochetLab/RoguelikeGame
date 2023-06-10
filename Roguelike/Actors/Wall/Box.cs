using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;
using Roguelike.World;

namespace Roguelike.Actors;

/// <summary>
/// Данный класс - класс стены.
/// </summary>
public class Box : Actor, IActorCreatable<Box>, IDamageable
{
    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public Box(BaseGame game) : base(game)
    { }

    public override void Initialize()
    {
        base.Initialize();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Box");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Solid;
    }

    public static Box Create(BaseGame game) => new(game);

    public void TakeDamage(float damage)
    {
        Dispose();
    }
}
