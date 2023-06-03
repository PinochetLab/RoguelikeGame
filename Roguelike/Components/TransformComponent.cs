using Roguelike.VectorUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Roguelike.Components
{
    public class TransformComponent : Component
    {

        public TransformComponent(Actor actor, Vector2Int position) : base(actor)
        {
            Position = position;
        }
        public Vector2Int Position { get; set; } = Vector2Int.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public float Angle { get; set; }
    }
}
