using System;
using Roguelike.Core;

namespace Roguelike.World.Providers.Generator.Dungeons;

internal static class CellHelpers
{
    public static void Fill(this Cell cell, int x, int y, ITileMap map, GeneratorParams parameters)
    {
        Tile[,] template;

        switch (cell.Type)
        {
            case CellType.Room:
                template = FillRoom(cell.Openings, parameters);
                break;
            case CellType.Corridor:
                template = FillCorridor(cell.Openings, parameters);
                break;
            default:
                return;
        }

        ApplyAttributes(cell, template);

        var cs = parameters.RoomSize;
        Iterate(template, (loc, tile) => map[x*cs + loc.X, y*cs + loc.Y] = tile);
    }

    private static void ApplyAttributes(Cell cell, Tile[,] template)
    {
        int w = template.GetLength(0), h = template.GetLength(1);
        var attr = cell.Attributes;

        if ((attr & AttributeType.Exit) == AttributeType.Exit)
            template[w/2, h/2].Attributes = AttributeType.Exit;
        else if ((attr & AttributeType.Entry) == AttributeType.Entry)
            template[w/2, h/2].Attributes = AttributeType.Entry;

        // apply monster spawns in center
        if ((attr & AttributeType.MobSpawn) == AttributeType.MobSpawn)
        {
            Iterate(template, (vector2Int, tile) =>
            {
                if (tile.MaterialType == MaterialType.Floor && (vector2Int.X > w/2.0f && vector2Int.Y < h/2.0f))
                {
                    template[vector2Int.X, vector2Int.Y].Attributes |= AttributeType.MobSpawn;
                    return false;
                }
                return true;
            });
        }

        // apply loot chests in corners of rooms
        if ((attr & AttributeType.Loot) != AttributeType.Loot) return;

        switch (cell.Type)
        {
            case CellType.Room:
                template[1, 1].Attributes |= AttributeType.Loot;
                template[w - 2, h - 2].Attributes |= AttributeType.Loot;
                break;
            case CellType.Corridor:
                template[w/2, h/2].Attributes |= AttributeType.Loot;
                break;
            case CellType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static Tile[,] FillCorridor(Direction openings, GeneratorParams parameters)
    {
        var template = MakeTemplate(parameters.RoomSize);
        var openingsArray = openings.ToDirectionsArray();

        // carve the walls first, so that
        foreach (var direction in openingsArray)
            CarveCorridors(direction, template, parameters, true);

        // we can do another pass with the floors later
        foreach (var direction in openingsArray)
            CarveCorridors(direction, template, parameters);

        MakeOpenings(template, openings, parameters);

        return template;
    }

    private static void CarveCorridors(Direction direction, Tile[,] template, GeneratorParams parameters, bool fillWalls = false)
    {
        var cellSize = parameters.RoomSize;
        switch (direction)
        {
            case Direction.West:
            {
                var endX = fillWalls ? template.GetLength(0)/2 + 1 : template.GetLength(0)/2;
                for (var x = 0; x <= endX; x++)
                {
                    if (fillWalls)
                    {
                        template[x, cellSize/2 - 1] = Tile.Wall;
                        template[x, cellSize/2 + 1] = Tile.Wall;
                        template[x, cellSize/2] = Tile.Wall;
                    }
                    else
                        template[x, cellSize/2] = Tile.Floor;
                }

                break;
            }
            case Direction.East:
            {
                var startX = fillWalls ? template.GetLength(0)/2 - 1 : template.GetLength(0)/2;
                for (var x =startX; x < template.GetLength(0); x++)
                {
                    if (fillWalls)
                    {
                        template[x, cellSize/2 - 1] = Tile.Wall;
                        template[x, cellSize/2 + 1] = Tile.Wall;
                        template[x, cellSize/2] = Tile.Wall;
                    }
                    else
                        template[x, cellSize/2] = Tile.Floor;
                }

                break;
            }
            case Direction.North:
            {
                var endY = fillWalls ? template.GetLength(1)/2 + 1 : template.GetLength(1)/2;
                for (var y = 0; y < endY; y++)
                {
                    if (fillWalls)
                    {
                        template[cellSize/2 - 1, y] = Tile.Wall;
                        template[cellSize/2 + 1, y] = Tile.Wall;
                        template[cellSize/2, y] = Tile.Wall;
                    }
                    else
                        template[cellSize/2, y] = Tile.Floor;
                }

                break;
            }
            case Direction.South:
            {
                var startY = fillWalls ? template.GetLength(1)/2 - 1 : template.GetLength(1)/2;
                for (var y = startY; y < template.GetLength(1); y++)
                {
                    if (fillWalls)
                    {
                        template[cellSize/2 - 1, y] = Tile.Wall;
                        template[cellSize/2 + 1, y] = Tile.Wall;
                        template[cellSize/2, y] = Tile.Wall;
                    }
                    else
                        template[cellSize/2, y] = Tile.Floor;
                }

                break;
            }
        }
    }

    private static Tile[,] MakeTemplate(int size, Tile tileType = default(Tile))
    {
        var template = new Tile[size, size];

        for (var x = 0; x < template.GetLength(0); x++)
        for (var y = 0; y < template.GetLength(1); y++)
            template[x, y] = tileType;

        return template;
    }

    private static void MakeOpenings(Tile[,] template, Direction openings, GeneratorParams parameters)
    {
        var size = parameters.RoomSize - 1;
        foreach (var opening in openings.ToDirectionsArray())
        {
            int x = 0, y = 0;

            switch (opening)
            {
                case Direction.North:
                    x = size/2;
                    break;
                case Direction.East:
                    x = size;
                    y = size/2;
                    break;
                case Direction.South:
                    x = size/2;
                    y = size;
                    break;
                case Direction.West:
                    y = size/2;
                    break;
                case Direction.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(openings));
            }

            template[x,y] = Tile.Floor;
        }
    }

    private static Tile[,] FillRoom(Direction openings, GeneratorParams parameters)
    {
        var size = parameters.RoomSize;
        var template = MakeTemplate(size, Tile.Wall);

        for (var x = 1; x < template.GetLength(0) - 1; x++)
        for (var y = 1; y < template.GetLength(1) - 1; y++)
            template[x, y] = Tile.Floor;

        MakeOpenings(template, openings, parameters);

        return template;
    }

    private static void Iterate(Tile[,] template, Func<Vector2Int, Tile, bool> callback)
    {
        for (var xPos = 0; xPos < template.GetLength(0); xPos++)
        for (var yPos = 0; yPos < template.GetLength(1); yPos++)
            if (!callback(new Vector2Int {X = xPos, Y = yPos}, template[xPos, yPos]))
                return;
    }

    private static void Iterate(Tile[,] template, Func<Vector2Int, Tile, Tile> callback)
    {
        for (var xPos = 0; xPos < template.GetLength(0); xPos++)
        for (var yPos = 0; yPos < template.GetLength(1); yPos++)
            template[xPos, yPos] = callback(new Vector2Int{X = xPos, Y = yPos}, template[xPos, yPos]);
    }
}