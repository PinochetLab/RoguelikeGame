using Roguelike.Actors.InventoryUtils;
using Roguelike.Actors.InventoryUtils.Items;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.VectorUtility;

namespace Roguelike.Actors;

public class ItemHolder : Actor
{

    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public override void OnStart()
    {
        base.OnStart();

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.LoadTexture("KFC");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
        collider.OnTriggerEnter += OnTriggerEnter;
    }

    public void OnTriggerEnter(ColliderComponent collider)
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

    public override void Draw(float delta)
    {
        base.Draw(delta);
    }
}
