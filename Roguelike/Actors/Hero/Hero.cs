using Roguelike.VectorUtility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Roguelike.Field;
using Roguelike.Components;
using Roguelike.Components.ColliderComponent;

namespace Roguelike
{
    public class Hero : Actor
    {

        private SpriteComponent spriteComponent;

        private ColliderComponent collider;

        private float speed = 200;

        public Hero(Vector2Int position) : base(position) { }

        public override void OnStart()
        {
            base.OnStart();

            spriteComponent = new SpriteComponent(this);
            spriteComponent.LoadTexture("HeroSprite");

            //collider = new ColliderComponent(this);
        }

        private void OnTrigger(ColliderComponent other) {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Vector2Int direction = Vector2Int.Zero;

            if (Input.IsDown(Keys.D))
            {
                direction = Vector2Int.Right;
            }
            else if (Input.IsDown(Keys.A))
            {
                direction = Vector2Int.Left;
            }
            else if (Input.IsDown(Keys.W))
            {
                direction = Vector2Int.Up;
            }
            else if (Input.IsDown(Keys.S))
            {
                direction = Vector2Int.Down;
            }

            if (direction != Vector2Int.Zero)
            {
                if (!ColliderManager.ContainsSolid(Transform.Position + direction))
                {
                    Transform.Position += direction;
                    if (direction == Vector2Int.Right)
                    {
                        spriteComponent.FlipX = false;
                    }
                    else if (direction == Vector2Int.Left)
                    {
                        spriteComponent.FlipX = true;
                    }
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
