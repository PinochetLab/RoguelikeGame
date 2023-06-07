using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;
using Roguelike.World;

namespace Roguelike.Actors;

/// <summary>
/// Данный класс - класс главного персонажа.
/// </summary>
public class Hero : Actor, IActorCreatable<Hero>
{
    /// <summary>
    /// Тэг главного персонажа.
    /// </summary>
    public const string HeroTag = "Hero";
    public override string Tag => HeroTag;

    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public Hero(BaseGame game) : base(game)
    { }

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("HeroSprite");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
    }

    public override void Update(GameTime time)
    {
        base.Update(time);

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
    }

    public static Hero Create(BaseGame game) => new Hero(game);
}
