using Microsoft.Xna.Framework;
using Roguelike.Components;
using Roguelike.VectorUtility;
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

        public TransformComponent Transform;

        public Actor(Vector2Int position) {
            Transform = new TransformComponent(this, position);
            RoguelikeGame.AddActor(this);
            OnStart();
        }

        public virtual void OnStart() {

        }

        public virtual void Update(float deltaTime) {

        }

        public virtual void Draw(float deltaTime) {

        }

        public virtual void Destroy() {
            RoguelikeGame.RemoveActor(this);
        }
    }
}
