using System;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;
public class WeaponSlot : Actor, IActorCreatable<WeaponSlot>
{
    public SpriteComponent SpriteComponent;

    public WeaponSlot(BaseGame game) : base(game)
    { }

    public Vector2Int Offset
    {
        get => SpriteComponent.Offset;
        set => SpriteComponent.Offset = value;
    }

    public override void OnStart()
    {
        base.OnStart();

        SpriteComponent = AddComponent<SpriteComponent>();
        SpriteComponent.SetTexture("Sword");
        SpriteComponent.DrawOrder = 4;
    }

    public static WeaponSlot Create(BaseGame game) => new(game);
}
