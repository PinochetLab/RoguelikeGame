using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

public class WeaponSlot : Actor, IActorCreatable<WeaponSlot>
{
    public SpriteComponent SpriteComponent;

    public WeaponSlot(BaseGame game) : base(game)
    {
    }

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