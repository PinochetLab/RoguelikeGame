using Roguelike.Components.ColliderComponent;
using Roguelike.VectorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors.Wall {
    public class Wall : Actor {

        private SpriteComponent spriteComponent;

        private ColliderComponent collider;

        public Wall(Vector2Int position) : base(position) { }

        public override void OnStart() {
            base.OnStart();

            spriteComponent = new SpriteComponent(this);
            spriteComponent.LoadTexture("Wall");

            collider = new ColliderComponent(this, ColliderType.Solid);
            collider.Type = ColliderType.Solid;
        }

        public override void Draw(float deltaTime) {
            base.Draw(deltaTime);

            spriteComponent.Draw();
        }
    }
}
