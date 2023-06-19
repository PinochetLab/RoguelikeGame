using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Roguelike.Actors.InventoryUtils;
using Roguelike.Actors.InventoryUtils.Items;
using Roguelike.Commands;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;
using Roguelike.Field;

namespace Roguelike.Actors;

/// <summary>
///     Данный класс - класс главного персонажа.
/// </summary>
public class Hero : Actor, IActorCreatable<Hero>, IDamageable
{
    private ColliderComponent collider;

    private HealthComponent healthComponent;

    private Item item;

    private SpriteComponent itemSpriteComponent;

    private KeyboardStateExtended keyState;

    private SpriteComponent spriteComponent;

    private WeaponItem weaponItem;

    public Hero(BaseGame game) : base(game)
    {
        Instance = this;
    }

    public static Hero Instance { get; private set; }

    public WeaponSlot WeaponSlot { get; set; }

    /// <summary>
    ///     Тэг главного персонажа.
    /// </summary>

    public override string Tag => Tags.HeroTag;

    public Vector2Int CurrentDirection { get; private set; } = Vector2Int.Right;


    public Item Item
    {
        get => item;
        set
        {
            switch (value)
            {
                case null:
                    WeaponSlot.SpriteComponent.Visible = false;
                    itemSpriteComponent.Visible = false;
                    item = null;
                    weaponItem = null;
                    break;
                case WeaponItem wi:
                    WeaponSlot.SpriteComponent.Visible = true;
                    WeaponSlot.SpriteComponent.SetTexture(value.TextureName);
                    itemSpriteComponent.Visible = false;
                    item = wi;
                    weaponItem = wi;
                    break;
                default:
                    WeaponSlot.SpriteComponent.Visible = false;
                    itemSpriteComponent.Visible = true;
                    itemSpriteComponent.SetTexture(value.TextureName);
                    item = value;
                    weaponItem = null;
                    break;
            }
        }
    }

    public static Hero Create(BaseGame game)
    {
        return new Hero(game);
    }

    public void TakeDamage(int damage)
    {
        healthComponent.Health -= damage;
    }

    public override void Initialize()
    {
        base.Initialize();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("HeroSprite");
        spriteComponent.DrawOrder = 2;

        itemSpriteComponent = AddComponent<SpriteComponent>();
        itemSpriteComponent.SetTexture("KFC");
        itemSpriteComponent.DrawOrder = 1;
        itemSpriteComponent.AdditionalScale = Vector2.One * 0.4f;
        itemSpriteComponent.Offset = Vector2Int.One * (FieldInfo.CellSize / 5);

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;

        WeaponSlot = World.CreateActor<WeaponSlot>(Transform.Position);
        WeaponSlot.Transform.Parent = Transform;

        healthComponent = AddComponent<HealthComponent>();
        healthComponent.OnDeath += GameOver;
        healthComponent.OnHealthChange += OnHealthChange;
        healthComponent.Initialize();
    }


    private void GameOver()
    {
        Inventory.Clear();
        Transform.Position = FieldInfo.Center;
        Dispose();
    }

    public void MoveDirection(Direction direction)
    {
        WeaponSlot.Transform.Direction = direction;

        switch (direction)
        {
            case Direction.Right:
                spriteComponent.FlipX = false;
                itemSpriteComponent.DrawOrder = 1;
                break;
            case Direction.Left:
                spriteComponent.FlipX = true;
                itemSpriteComponent.DrawOrder = 3;
                break;
            case Direction.Up:
                break;
            case Direction.Down:
                break;
        }

        if (direction != Vector2Int.Zero) CurrentDirection = direction;

        if (direction == Vector2Int.Zero ||
            (World.Colliders.ContainsSolid(Transform.Position + direction) &&
             !World.Colliders.ContainsSolid(Transform.Position))) return;

        Transform.Position += direction;
    }

    public override void Update(GameTime time)
    {
        base.Update(time);

        var direction = Vector2Int.Zero;
        keyState = KeyboardExtended.GetState();
        var state = keyState;


        if (state.WasKeyJustUp(Keys.Space)) Game.World.Commands.SetCommand(new AttackCommand(this));
        Game.World.Commands.Invoke();

        if (state.WasKeyJustUp(Keys.D))
            Game.World.Commands.SetCommand(new MoveRightCommand(this));
        else if (state.WasKeyJustUp(Keys.A))
            Game.World.Commands.SetCommand(new MoveLeftCommand(this));
        else if (state.WasKeyJustUp(Keys.W))
            Game.World.Commands.SetCommand(new MoveUpCommand(this));
        else if (state.WasKeyJustUp(Keys.S)) Game.World.Commands.SetCommand(new MoveDownCommand(this));
        Game.World.Commands.Invoke();
    }

    public void TryAttack()
    {
        //TODO different attacks on different keys or something
        var attack = weaponItem?.Attacks.FirstOrDefault();
        attack?.Attack(this, CurrentDirection);
    }

    private void OnHealthChange()
    {
        World.Stats.SetHealth(healthComponent.Health);
    }

    public void UpdateHealth(int health)
    {
        healthComponent.SetMaxHealth(health, true);
    }
}