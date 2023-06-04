using Roguelike.Actors.InventoryUtils;
using Roguelike.Actors.InventoryUtils.Items;
using Roguelike.Components.ColliderComponent;
using Roguelike.VectorUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors
{
    public class ItemHolder : Actor
    {

        private SpriteComponent spriteComponent;

        private ColliderComponent collider;

        public ItemHolder(Vector2Int position) : base(position)
        {

        }

        public override void OnStart()
        {
            base.OnStart();

            spriteComponent = new SpriteComponent(this);
            spriteComponent.LoadTexture("KFC");

            collider = new ColliderComponent(this, ColliderType.Trigger);
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

        public override void Draw(float deltaTime)
        {
            base.Draw(deltaTime);

            spriteComponent.Draw();
        }
    }
}
