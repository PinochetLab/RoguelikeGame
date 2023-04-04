using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    public class Actor
    {

        protected Vector2 position { get; set; } = Vector2.Zero;
        protected Vector2 scale { get; set; } = Vector2.One;
        protected float angle { get; set; } = 0;

        protected Actor()
        {

        }

        protected Actor(Vector2 position, Vector2 scale, float angle)
        {
            this.position = position;
            this.scale = scale;
            this.angle = angle;
            RoguelikeGame.AddActor(this);
            OnStart();
        }

        public virtual void OnStart()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void Draw(float deltaTime)
        {

        }

        public virtual void Destroy()
        {
            RoguelikeGame.RemoveActor(this);
        }
    }
}
