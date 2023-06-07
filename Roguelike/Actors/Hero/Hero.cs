using Roguelike.VectorUtility;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using System;
using System.Diagnostics;

namespace Roguelike.Actors;

/// <summary>
/// Данный класс - класс главного персонажа.
/// </summary>
public class Hero : Actor
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

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("HeroSprite");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;

        weaponSlot = Create<WeaponSlot>(Transform.Position);
        weaponSlot.Transform.Parent = Transform;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

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

        if (direction == Vector2Int.Zero || ColliderManager.ContainsSolid(Transform.Position + direction)) return;

        currentDirection = direction;

        Transform.Position += direction;

        if (direction == Vector2Int.Right)
        {
            spriteComponent.FlipX = false;
        }
        else if (direction == Vector2Int.Left)
        {
            spriteComponent.FlipX = true;
        }

        RoguelikeGame.MoveAll();
    }

    private void ShootLogic()
    {
        var state = keyState;

        if (!state.WasKeyJustUp(Keys.Space)) return;

        var bulletPosition = Transform.Position + currentDirection;

        if (!ColliderManager.ContainsSolid(bulletPosition) &&
            !ColliderManager.Contains<Arrow>(bulletPosition))
        {
            var arrow = Create<Arrow>(bulletPosition);
            arrow.Transform.Angle = MathF.Atan2(currentDirection.Y, currentDirection.X);
        }
    }
}
