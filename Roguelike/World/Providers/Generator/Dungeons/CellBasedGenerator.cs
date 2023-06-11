using System.Collections.Generic;
using System.Linq;
using Roguelike.Core;
using Roguelike.World.Providers.Generator.Utils;

namespace Roguelike.World.Providers.Generator.Dungeons;

public class CellBasedGenerator : ILevelGenerator
{
    private readonly GeneratorParams parameters;
    private readonly MersennePrimeRandom random;

    private Cell[,] cells;

    public CellBasedGenerator() : this(GeneratorParams.Default)
    { }

    public CellBasedGenerator(GeneratorParams parameters)
    {
        this.parameters = parameters;
        random = new MersennePrimeRandom(parameters.Seed);
    }

    public ITileMap Generate(int width, int height)
    {
        var w = width / parameters.RoomSize;
        var h = height / parameters.RoomSize;

        cells = new Cell[w, h];

        var startLoc = new Vector2Int { X = w / 2, Y = h / 2 };
        cells[startLoc.X, startLoc.Y] = Cell.FourWayRoom();

        if (parameters.Exits)
            cells[startLoc.X, startLoc.Y].Attributes = AttributeType.Entry;

        var unprocessed = new Queue<Vector2Int>();
        unprocessed.Enqueue(startLoc);

        while (unprocessed.Count > 0)
        {
            var location = unprocessed.Dequeue();
            var cell = cells[location.X, location.Y];

            foreach (var opening in cell.Openings.ToDirectionsArray())
            {
                var newLocation = opening.GetLocation(location);
                var newCell = DetermineCellType(newLocation, opening);

                if (newCell.Type == CellType.None) continue;

                cells[newLocation.X, newLocation.Y] = ApplyAttributes(newCell);
                unprocessed.Enqueue(newLocation);
            }
        }

        var secondExitPlaced = !parameters.Exits;

        var chance = 10;
        var map = new TileArrayMap(width, height);
        var len1 = cells.GetLength(0);
        var len2 = cells.GetLength(1);

        for (var x = 0; x < len1; x++)
        for (var y = 0; y < len2; y++)
        {
            var cell = cells[x, y];

            if (!secondExitPlaced)
            {
                var spawnExit = random.Chance(chance);
                if (cell.Type != CellType.None && (x <= w * 0.15 || x >= w * 0.65) && spawnExit)
                {
                    cell.Attributes = AttributeType.Exit;
                    secondExitPlaced = true;
                }
                else if (!spawnExit)
                    chance += (int)(chance * 0.25f);
            }

            cell.Fill(x, y, map, parameters);
        }

        return map;
    }


    private Cell ApplyAttributes(Cell newCell)
    {
        if (random.Chance(parameters.MobSpawns) && (!parameters.MobsInRoomsOnly || newCell.Type == CellType.Room))
            newCell.Attributes |= AttributeType.MobSpawn;

        if (random.Chance(parameters.Loot) && newCell.Type == CellType.Room)
            newCell.Attributes |= AttributeType.Loot;

        if (random.Chance(parameters.Doors))
            newCell.Attributes |= AttributeType.Doors;

        return newCell;
    }

    // pick a cell type that will connect as many rooms as possible
    private Cell DetermineCellType(Vector2Int location, Direction direction)
    {
        var locationInBounds = location.X >= 0
                               && location.X < cells.GetLength(0)
                               && location.Y >= 0
                               && location.Y < cells.GetLength(0);

        if (locationInBounds && cells[location.X, location.Y].Type == CellType.None)
            return new Cell
            {
                Type = random.Chance(parameters.RoomChance) ? CellType.Room : CellType.Corridor,
                Openings = FindValidConnections(direction, location),
                Attributes = AttributeType.None,
            };

        return default;
    }

    private Direction FindValidConnections(Direction dir, Vector2Int loc)
    {
        var list = new List<Direction>();

        var startDir = dir;

        dir = dir.TurnLeft();
        for (var i = 0; i < DirectionHelpers.Directions.Length - 1; i++)
        {
            var newLoc = dir.GetLocation(loc);

            if (newLoc.X >= 0 && newLoc.X < cells.GetLength(0) && newLoc.Y >= 0 && newLoc.Y < cells.GetLength(1))
            {
                var cell = cells[newLoc.X, newLoc.Y];

                if (cell.Type == CellType.None || cell.Openings.Facing(dir))
                    list.Add(dir);
            }

            dir = dir.TurnRight();
        }


        var connectsToMake = list.Count > 0 ? random.Next(1, list.Count + 1) : 0;
        var validConnections = list.Take(connectsToMake).Concat(new[] { startDir.TurnAround() }).ToArray();

        // close the openings in the neighbor cells that we didn't make
        foreach (var connectToUndo in list.Skip(connectsToMake))
        {
            var newLoc = connectToUndo.GetLocation(loc);
            var dirToUndo = connectToUndo.TurnAround();

            cells[newLoc.X, newLoc.Y].Openings ^= dirToUndo;
        }

        return validConnections.ToDirectionFlag();
    }
}