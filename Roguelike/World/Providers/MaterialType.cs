using System.Text.Json.Serialization;

namespace Roguelike.World.Providers;

/// <summary>
/// The material of the tile
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MaterialType : byte
{
    /// <summary>
    /// Nothing
    /// </summary>
    Air = 0x00,
    /// <summary>
    /// Traversable ground
    /// </summary>
    Floor,
    /// <summary>
    /// An impassable, indestructible wall
    /// </summary>
    Wall,
    /// <summary>
    /// A destructible will
    /// </summary>
    BreakableWall,
}