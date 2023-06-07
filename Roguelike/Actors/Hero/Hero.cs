using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using System;
using Roguelike.Core;

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

    private WeaponSlot weaponSlot;

    private Vector2Int currentDirection = Vector2Int.Right;

    private KeyboardStateExtended keyState;

    public Hero(BaseGame game) : base(game)
    { }

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("HeroSprite");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;

        weaponSlot = World.CreateActor<WeaponSlot>(Transform.Position);
        weaponSlot.Transform.Parent = Transform;
    }

    public override void Update(GameTime time)
    {
        base.Update(time);

        keyState = KeyboardExtended.GetState();

        MoveLogic();

        ShootLogic();
    }

    private void MoveLogic()
    {
        var direction = Vector2Int.Zero;
        var state = keyState;

        if (state.WasKeyJustUp(Keys.D))
        {
            direction = Vector2Int.Right;
            weaponSlot.Transform.Angle = 0;
        }
        else if (state.WasKeyJustUp(Keys.A))
        {
            direction = Vector2Int.Left;
            weaponSlot.Transform.Angle = MathF.PI;
        }
        else if (state.WasKeyJustUp(Keys.W))
        {
            direction = Vector2Int.Up;
            weaponSlot.Transform.Angle = -MathF.PI / 2;
        }
        else if (state.WasKeyJustUp(Keys.S))
        {
            direction = Vector2Int.Down;
            weaponSlot.Transform.Angle = MathF.PI / 2;
        }

        if (direction == Vector2Int.Zero || World.Colliders.ContainsSolid(Transform.Position + direction)) return;

        currentDirection = direction;

        Transform.Position += direction;

        if (direction == Vector2Int.Right)
        {
            spriteComponent.FlipX = false;
            weaponSlot.Transform.FlipX = false;
        }
        else if (direction == Vector2Int.Left)
        {
            spriteComponent.FlipX = true;
            weaponSlot.Transform.FlipX = true;
        }

        World.MoveAll();
    }

    private void ShootLogic()
    {
        var state = keyState;

        if (!state.WasKeyJustUp(Keys.Space)) return;

        var bulletPosition = Transform.Position + currentDirection;

        if (!World.Colliders.ContainsSolid(bulletPosition) &&
            !World.Colliders.Contains<Arrow>(bulletPosition))
        {
            var arrow = World.CreateActor<Arrow>(bulletPosition);
            arrow.Transform.Angle = MathF.Atan2(currentDirection.Y, currentDirection.X);
        }
    }

    public static Hero Create(BaseGame game) => new Hero(game);
}
