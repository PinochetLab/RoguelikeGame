using Roguelike.VectorUtility;
using Microsoft.Xna.Framework;

namespace Roguelike.Components;

public class TransformComponent : Component
{
    public Vector2Int Position { get; set; } = Vector2Int.Zero;
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Angle { get; set; } = 0;
}
