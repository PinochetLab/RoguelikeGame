using Roguelike.Components.Colliders;
using Roguelike.VectorUtility;
using Roguelike.Components.Sprites;

namespace Roguelike.Actors;

public class Wall : Actor {

    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.LoadTexture("Wall");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Solid;
    }

    public override void Draw()
    {
        base.Draw();
    }
}
