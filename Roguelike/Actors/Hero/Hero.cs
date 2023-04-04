using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Roguelike
{
    public class Hero : Actor
    {

        private SpriteComponent spriteComponent;

        private float speed = 200;

        public static Hero Spawn(Vector2 position)
        {
            return Spawn(position, Vector2.One, 0);
        }

        public static Hero Spawn(Vector2 position, Vector2 scale, float angle)
        {
            return new Hero(position, scale, angle);
        }

        private Hero(Vector2 position, Vector2 scale, float angle) : base(position, scale, angle)
        {

        }

        public override void OnStart()
        {
            base.OnStart();

            spriteComponent = new SpriteComponent("hero");
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            float hor = (Input.IsPressed(Keys.D) ? 1 : 0) + (Input.IsPressed(Keys.A) ? -1 : 0);
            float ver = (Input.IsPressed(Keys.W) ? -1 : 0) + (Input.IsPressed(Keys.S) ? 1 : 0);

            Vector2 direction = Vector2.UnitX * hor + Vector2.UnitY * ver;
            if (direction.Length() > 0) direction.Normalize();

            if (hor != 0) spriteComponent.FlipX = hor < 0;

            position += direction * speed * deltaTime;
        }

        public override void Draw(float deltaTime)
        {
            base.Draw(deltaTime);

            spriteComponent.Draw(position);
        }
    }
}
