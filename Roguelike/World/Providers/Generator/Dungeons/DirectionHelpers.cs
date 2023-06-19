using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Core;

namespace Roguelike.World.Providers.Generator.Dungeons;

internal static class DirectionHelpers
{
    public static readonly Direction[] Directions =
        Enum.GetValues<Direction>().Where(x => x != Direction.None).ToArray();

    public static bool HasFlag(this Direction dir, Direction other)
    {
        var result = (byte)dir & (byte)other;
        return result == (byte)other;
    }

    /// <summary>
    ///     Does the source direction face the 'other' direction
    /// </summary>
    /// <param name="source"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool Facing(this Direction source, Direction other)
    {
        return source.ToDirectionsArray().Any(x =>
        {
            return x switch
            {
                Direction.None => false,
                Direction.North => other.HasFlag(Direction.South),
                Direction.East => other.HasFlag(Direction.West),
                Direction.South => other.HasFlag(Direction.North),
                Direction.West => other.HasFlag(Direction.East),
                _ => throw new ArgumentOutOfRangeException(nameof(x))
            };
        });
    }

    public static Direction ToDirectionFlag(this IEnumerable<Direction> source)
    {
        return source.Aggregate(Direction.None, (agg, item) => agg | item);
    }

    public static Vector2Int GetLocation(this Direction dir, Vector2Int previous)
    {
        int x = 0, y = 0;

        switch (dir)
        {
            case Direction.North:
                y = -1;
                break;
            case Direction.East:
                x = 1;
                break;
            case Direction.South:
                y = 1;
                break;
            case Direction.West:
                x = -1;
                break;
            case Direction.None: break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dir));
        }

        return new Vector2Int { X = previous.X + x, Y = previous.Y + y };
    }

    public static Direction[] ToDirectionsArray(this Direction dir)
    {
        return Directions.Where(x => dir.HasFlag(x)).ToArray();
    }

    public static Direction TurnAround(this Direction dir)
    {
        return dir.TurnRight().TurnRight();
    }

    public static Direction TurnLeft(this Direction dir)
    {
        return dir.TurnAround().TurnRight();
    }

    public static Direction TurnRight(this Direction dir)
    {
        return dir switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => throw new ArgumentOutOfRangeException(nameof(dir))
        };
    }
}