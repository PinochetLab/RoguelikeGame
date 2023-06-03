using Microsoft.Xna.Framework.Graphics;
using Roguelike.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    public abstract class Component
    {
        public Actor Owner { get; init; }

        public TransformComponent Transform => Owner?.Transform;

        protected Component(Actor actor)
        {
            Owner = actor;
        }
    }
}
