using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Roguelike.World.Providers;

/// <summary>
/// A representation of a tile
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct Tile
{
    /// <summary>
    /// The material type of this tile
    /// </summary>
    [JsonInclude]
    [FieldOffset(0)]
    public MaterialType MaterialType;

    /// <summary>
    /// The meta data info about this tile
    /// </summary>
    [JsonInclude]
    [FieldOffset(1)]
    public AttributeType Attributes;

    /// <summary>
    /// A <see cref="Tile"/> with a floor <see cref="MaterialType"/>
    /// </summary>
    public static readonly Tile Floor = new() {MaterialType = MaterialType.Floor};

    /// <summary>
    /// A <see cref="Tile"/> with a Wall <see cref="MaterialType"/>
    /// </summary>
    public static readonly Tile Wall = new() {MaterialType = MaterialType.Wall};

    /// <summary>
    /// A <see cref="Tile"/> with an Air <see cref="MaterialType"/>
    /// </summary>
    public static readonly Tile Air = new() {MaterialType = MaterialType.Air};
}