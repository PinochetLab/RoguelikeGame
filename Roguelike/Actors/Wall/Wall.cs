using Roguelike.Components.Colliders;
using Roguelike.VectorUtility;
using Roguelike.Components.Sprites;

namespace Roguelike.Actors;

/// <summary>
/// Данный класс - класс стены.
/// </summary>
public class Wall : Actor {

    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Wall");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Solid;
    }
}
