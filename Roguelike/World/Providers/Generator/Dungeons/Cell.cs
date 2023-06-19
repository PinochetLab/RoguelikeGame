using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Roguelike.World.Providers.Generator.Dungeons;

[DebuggerDisplay("{Type} Cell Exits {Openings}"), StructLayout(LayoutKind.Explicit)]
public struct Cell
{
    [FieldOffset(0)] public Direction Openings;

    [FieldOffset(1)] public CellType Type;

    [FieldOffset(2)] public AttributeType Attributes;

    public static Cell FourWayRoom()
    {
        return new()
        {
            Type = CellType.Room,
            Openings = Direction.North | Direction.South | Direction.East | Direction.West
        };
    }
}