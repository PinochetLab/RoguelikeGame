using Roguelike.Components;
using Roguelike.VectorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors
{
    public class CanvasActor : Actor
    {
        public CanvasActor(Vector2Int position) : base(position)
        {
            Transform = new TransformComponent(this, position);
            RoguelikeGame.AddCanvasActor(this);
            OnStart();
        }

        public virtual void Destroy()
        {
            RoguelikeGame.RemoveCanvasActor(this);
        }
    }
}
