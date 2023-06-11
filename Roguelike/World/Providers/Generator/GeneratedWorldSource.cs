using System;
using System.Linq;
using Roguelike.World.Providers.Generator.Dungeons;

namespace Roguelike.World.Providers.Generator;

public class GeneratedWorldSource : IWorldSource
{
    private readonly int sizeX;
    private readonly int sizeY;
    private readonly GeneratorParams parameters;

    public GeneratedWorldSource(int sizeX, int sizeY, GeneratorParams parameters)
    {
        if (sizeX <= 0)
            throw new ArgumentOutOfRangeException(nameof(sizeX));
        if (sizeY <= 0)
            throw new ArgumentOutOfRangeException(nameof(sizeY));
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.parameters = parameters;
    }

    public ITileMap GetMapRepresentation()
    {
        var generator = new CellBasedGenerator(parameters);
        return generator.Generate(sizeX, sizeY);
    }
}