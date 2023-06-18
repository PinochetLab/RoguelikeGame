using Roguelike.Actors.InventoryUtils;
using Roguelike.Actors.InventoryUtils.Items;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

/// <summary>
///     Данный класс - класс игрового объекта, содержащего предмет. К такому объекту можно подойти и получить хранимый
///     предмет.
/// </summary>
public class ItemHolder : Actor, IActorCreatable<ItemHolder>
{
    private ColliderComponent collider;
    private SpriteComponent spriteComponent;

    public ItemHolder(BaseGame game) : base(game)
    {
    }

    public static ItemHolder Create(BaseGame game)
    {
        return new(game);
    }

    public override void Initialize()
    {
        base.Initialize();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("KFC");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
        collider.OnTriggerEnter += OnTriggerEnter;
    }

    private void OnTriggerEnter(ColliderComponent collider)
    {
        if (collider.Owner.Tag == Hero.HeroTag)
            if (Inventory.HasFreePlace())
            {
                Inventory.Add(new ItemKFC());
                Dispose();
            }
    }
}