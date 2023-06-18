using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

/// <summary>
///     Данный класс - класс стены.
/// </summary>
public class Box : Actor, IActorCreatable<Box>, IDamageable
{
    private ColliderComponent collider;
    private SpriteComponent spriteComponent;

    public Box(BaseGame game) : base(game)
    {
    }

    public static Box Create(BaseGame game)
    {
        return new(game);
    }

    public void TakeDamage(int damage)
    {
        Dispose();
    }

    public override void Initialize()
    {
        base.Initialize();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Box");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Solid;
    }
}