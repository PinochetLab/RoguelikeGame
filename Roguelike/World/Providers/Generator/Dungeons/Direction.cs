using System;

namespace Roguelike.World.Providers.Generator.Dungeons;

/// <summary>
///     Direction flags. Laid out so that a right turn is the same as going up one flag
/// </summary>
[Flags]
public enum Direction : byte
{
    None = 0,
    North = 0x01,
    East = 0x02,
    South = 0x04,
    West = 0x08
}