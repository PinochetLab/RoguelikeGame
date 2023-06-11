using System;
using System.Text.Json.Serialization;
using Roguelike.World.Providers.Generator.Utils;

namespace Roguelike.World.Providers;

/// <summary>
/// Tile meta data
/// </summary>
[Flags]
[JsonConverter(typeof(EnumWithFlagsJsonConverter<AttributeType>))]
public enum AttributeType : byte
{
    /// <summary>
    /// Nothing
    /// </summary>
    None = 0x00,
    /// <summary>
    /// An entrance to the dungeon
    /// </summary>
    Entry = 0x01,
    /// <summary>
    /// An exit to the dungeon
    /// </summary>
    Exit = 0x02,
    /// <summary>
    /// A loot/treasure spawn
    /// </summary>
    Loot = 0x04,
    /// <summary>
    /// A mob/AI spawn
    /// </summary>
    MobSpawn = 0x08,
    /// <summary>
    /// A bi directional doorway
    /// </summary>
    Doors = 0x10
}