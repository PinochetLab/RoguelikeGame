using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

/// <summary>
///     Данный класс отвечает за отображение ткущего предмета на игроке
/// </summary>
public class WeaponSlot : Actor, IActorCreatable<WeaponSlot>
{
    /// <summary>
    ///     Спрайт предмета
    /// </summary>
    public SpriteComponent SpriteComponent;

    public WeaponSlot(BaseGame game) : base(game)
    {
    }

    /// <summary>
    ///     Смещение спрайта относительно игрока
    /// </summary>
    public Vector2Int Offset
    {
        get => SpriteComponent.Offset;
        set => SpriteComponent.Offset = value;
    }

    public static WeaponSlot Create(BaseGame game)
    {
        return new WeaponSlot(game);
    }

    public override void Initialize()
    {
        base.Initialize();

        SpriteComponent = AddComponent<SpriteComponent>();
        SpriteComponent.SetTexture("Sword");
        SpriteComponent.DrawOrder = 4;
    }
}