using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;
using Roguelike.World;

namespace Roguelike.Actors;

/// <summary>
/// Данный класс - класс стены.
/// </summary>
public class Wall : Actor, IActorCreatable<Wall>
{
    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public Wall(BaseGame game) : base(game)
    { }

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Wall");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Solid;
    }

    public static Wall Create(BaseGame game) => new(game);
}
