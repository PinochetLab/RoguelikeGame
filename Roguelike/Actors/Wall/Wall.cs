using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

/// <summary>
///     Данный класс - класс стены.
/// </summary>
public class Wall : Actor, IActorCreatable<Wall>
{
    private ColliderComponent collider;
    private SpriteComponent spriteComponent;

    public Wall(BaseGame game) : base(game)
    {
    }

    public static Wall Create(BaseGame game)
    {
        return new Wall(game);
    }

    public override void Initialize()
    {
        base.Initialize();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Wall");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Solid;
    }
}