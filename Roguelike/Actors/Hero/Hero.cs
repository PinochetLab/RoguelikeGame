using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using System;
using Roguelike.Core;
using System.Collections;
using System.Threading.Tasks;
using System.Reflection;
using Roguelike.Actors.InventoryUtils.Items;

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

    private SpriteComponent itemSpriteComponent;

    private ColliderComponent collider;

    private WeaponSlot weaponSlot;

    private Vector2Int currentDirection = Vector2Int.Right;

    private KeyboardStateExtended keyState;

    public static Hero Instance;

    private WeaponItem weaponItem = null;
    private Item item = null;

    public Item Item
    {
        get => item;
        set
        {
            if (value == null)
            {
                weaponSlot.SpriteComponent.Visible = false;
                itemSpriteComponent.Visible = false;
                item = null;
                weaponItem = null;
            }
            else
            {
                if (value is WeaponItem wi)
                {
                    weaponSlot.SpriteComponent.Visible = true;
                    weaponSlot.SpriteComponent.SetTexture(value.TextureName);
                    itemSpriteComponent.Visible = false;
                    item = wi;
                    weaponItem = wi;
                }
                else
                {
                    weaponSlot.SpriteComponent.Visible = false;
                    itemSpriteComponent.Visible = true;
                    itemSpriteComponent.SetTexture(value.TextureName);
                    item = value;
                    weaponItem = null;
                }
            }
        }
    }

    public Hero(BaseGame game) : base(game)
    {
        Instance = this;
    }

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("HeroSprite");
        spriteComponent.DrawOrder = 2;

        itemSpriteComponent = AddComponent<SpriteComponent>();
        itemSpriteComponent.SetTexture("KFC");
        itemSpriteComponent.DrawOrder = 1;
        itemSpriteComponent.AdditionalScale = Vector2.One * 0.4f;
        itemSpriteComponent.Offset = Vector2Int.One * (Field.FieldInfo.CellSize / 5);

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

        if (weaponItem != null)
        {
            if (weaponItem.IsSword)
            {
                HitLogic();
            }
            else
            {
                ShootLogic();
            }
        }
    }

    private void MoveLogic()
    {
        var direction = Vector2Int.Zero;
        var state = keyState;

        if (state.WasKeyJustUp(Keys.D))
        {
            direction = Vector2Int.Right;
            weaponSlot.Transform.Angle = 0;
            spriteComponent.FlipX = false;
            itemSpriteComponent.DrawOrder = 1;
        }
        else if (state.WasKeyJustUp(Keys.A))
        {
            direction = Vector2Int.Left;
            weaponSlot.Transform.Angle = MathF.PI;
            spriteComponent.FlipX = true;
            itemSpriteComponent.DrawOrder = 3;
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
        

        if (direction == Vector2Int.Zero || 
            (World.Colliders.ContainsSolid(Transform.Position + direction) &&
            !World.Colliders.ContainsSolid(Transform.Position))) return;

        currentDirection = direction;

        Transform.Position += direction;

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
            arrow.Damage = weaponItem.Damage;
        }
    }

    private async void HitLogic()
    {
        var state = keyState;

        if (!state.WasKeyJustUp(Keys.Space)) return;

        var p = Transform.Position + currentDirection;
        var damageable = World.Colliders.Find<IDamageable>(p);
        damageable?.TakeDamage(weaponItem.Damage);

        Task.Run(Hit);
    }

    private async Task Hit()
    {
        for (var i = 0; i < 2; i++)
        {
            weaponSlot.Offset = currentDirection * 30;
            await Task.Delay(50);
            weaponSlot.Offset = Vector2Int.Zero;
            await Task.Delay(50);
        }
    }

    public static Hero Create(BaseGame game) => new Hero(game);
}
