using Roguelike.VectorUtility;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;

namespace Roguelike.Actors;

public class Hero : Actor
{
    public const string HeroTag = "Hero";
    public override string Tag { get => HeroTag; }

    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.LoadTexture("HeroSprite");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
    }

    public override void Update()
    {
        base.Update();

        var direction = Vector2Int.Zero;
        var state = KeyboardExtended.GetState();

        if (state.WasKeyJustUp(Keys.D))
        {
            direction = Vector2Int.Right;
        }
        else if (state.WasKeyJustUp(Keys.A))
        {
            direction = Vector2Int.Left;
        }
        else if (state.WasKeyJustUp(Keys.W))
        {
            direction = Vector2Int.Up;
        }
        else if (state.WasKeyJustUp(Keys.S))
        {
            direction = Vector2Int.Down;
        }

        if (direction == Vector2Int.Zero || ColliderManager.ContainsSolid(Transform.Position + direction)) return;

        Transform.Position += direction;
        if (direction == Vector2Int.Right)
        {
            spriteComponent.FlipX = false;
        }
        else if (direction == Vector2Int.Left)
        {
            spriteComponent.FlipX = true;
        }
        collider.UpdatePosition();

    }

    public override void Draw()
    {
        base.Draw();
    }
}
