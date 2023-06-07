using Roguelike.Actors.InventoryUtils;
using Roguelike.Actors.InventoryUtils.Items;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;
using Roguelike.World;

namespace Roguelike.Actors;

/// <summary>
/// Данный класс - класс игрового объекта, содержащего предмет. К такому объекту можно подойти и получить хранимый предмет.
/// </summary>
public class ItemHolder : Actor
{

    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("KFC");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
        collider.OnTriggerEnter += OnTriggerEnter;
    }

    private void OnTriggerEnter(ColliderComponent collider)
    {
        if (collider.Owner.Tag == Hero.HeroTag)
        {
            if (Inventory.HasFreePlace())
            {
                Inventory.Add(new ItemKFC());
                Destroy();
            }
        }
    }
}
