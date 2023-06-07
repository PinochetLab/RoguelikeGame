using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Components;
using Roguelike.VectorUtility;
using IMovable = Roguelike.Components.IMovable;
using System.Diagnostics;

namespace Roguelike.Actors;
public class Arrow : Actor, IMovable
{

    private SpriteComponent spriteComponent;

    private ColliderComponent collider;

    public override void Initialize(Vector2Int position)
    {
        base.Initialize(position);

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Arrow");

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
        collider.OnTriggerEnter += OnTriggerEnter;
    }

    public void Move()
    {
        Vector2Int direction = new Vector2(MathF.Cos(Transform.Angle), MathF.Sin(Transform.Angle));
        Debug.WriteLine(Transform.Position);
        Transform.Position += direction;
    }

    public void OnTriggerEnter(ColliderComponent other)
    {
        if (other.Type == ColliderType.Trigger) return;
        Destroy();
    }
}
