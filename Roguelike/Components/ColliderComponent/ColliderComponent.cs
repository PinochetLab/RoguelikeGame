using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.VectorUtility;

namespace Roguelike.Components.ColliderComponent {

    public enum ColliderType { Solid, Trigger }

    public class ColliderComponent : Component {

        public event Action<ColliderComponent> OnTriggerEnter;
        public event Action<ColliderComponent> OnTriggerExit;

        public ColliderType Type { get; set; }

        public Vector2Int CurrentPosition { get; set; }

        public ColliderComponent(Actor actor, ColliderType type) : base(actor) {
            CurrentPosition = Transform.Position;
            Type = type;

            if (!ColliderManager.ColliderMap.ContainsKey(CurrentPosition)) {
                ColliderManager.ColliderMap[CurrentPosition] = new();
            }

            ColliderManager.ColliderMap[CurrentPosition].Add(this);

            Debug.WriteLine(CurrentPosition.X + " " + CurrentPosition.Y);
        }

        public void UpdatePosition(){
            if (ColliderManager.ColliderMap.TryGetValue(CurrentPosition, out var list))
            { 
                list.Remove(this);
                foreach (var collider in list)
                {
                    collider.OnTriggerExit(this);
                    OnTriggerExit(collider);
                }
            }

            var position = Transform.Position;
            
            if (!ColliderManager.ColliderMap.ContainsKey(position)) {
                ColliderManager.ColliderMap[position] = new();
            }

            foreach (var collider in ColliderManager.ColliderMap[position]) {
                collider.OnTriggerEnter(this);
                OnTriggerEnter(collider);
            }

            ColliderManager.ColliderMap[position].Add(this);

            CurrentPosition = position;
        }
    }
}
