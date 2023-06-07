using System;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;
public class WeaponSlot : Actor, IActorCreatable<WeaponSlot>
{
    private SpriteComponent spriteComponent;

    public WeaponSlot(BaseGame game) : base(game)
    { }

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Bow3");
        spriteComponent.DrawOrder = 1;
    }

    public static WeaponSlot Create(BaseGame game) => new(game);
}
