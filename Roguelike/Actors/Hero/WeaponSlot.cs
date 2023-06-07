using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.Components.Sprites;

namespace Roguelike.Actors;
public class WeaponSlot : Actor
{
    private SpriteComponent spriteComponent;

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Bow3");
        spriteComponent.DrawOrder = 1;
    }
}
