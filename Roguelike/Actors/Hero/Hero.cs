using Roguelike.VectorUtility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Roguelike.Field;
using Roguelike.Components;
using Roguelike.Components.ColliderComponent;

namespace Roguelike
{
    public class Hero : Actor
    {

        public static string HeroTag = "Hero";
        public override string Tag { get => HeroTag; }

        private SpriteComponent spriteComponent;

        private ColliderComponent collider;

        private float speed = 200;

        public Hero(Vector2Int position) : base(position) { }

        public override void OnStart()
        {
            base.OnStart();

            spriteComponent = new SpriteComponent(this);
            spriteComponent.LoadTexture("HeroSprite");

            collider = new ColliderComponent(this, ColliderType.Solid);
        }

        private void OnTrigger(ColliderComponent other) {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            var direction = Vector2Int.Zero;
            var state = KeyboardExtended.GetState();

            if (state.WasKeyJustDown(Keys.D))
            {
                direction = Vector2Int.Right;
            }
            else if (state.WasKeyJustDown(Keys.A))
            {
                direction = Vector2Int.Left;
            }
            else if (state.WasKeyJustDown(Keys.W))
            {
                direction = Vector2Int.Up;
            }
            else if (state.WasKeyJustDown(Keys.S))
            {
                direction = Vector2Int.Down;
            }

            if (direction == Vector2Int.Zero || ColliderManager.ContainsSolid(Transform.Position + direction)) return;

            Transform.Position += direction;
            if (direction == Vector2Int.Right)
            {
                spriteComponent.FlipX = false;
            }
            else if (direction == Vector2Int.Left)
            {
                spriteComponent.FlipX = true;
            }
            collider.UpdatePosition();

        }

        public override void Draw(float deltaTime)
        {
            base.Draw(deltaTime);

            spriteComponent.Draw();
        }
    }
}
