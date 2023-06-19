using System.Text.Json;
using Microsoft.Xna.Framework;

namespace Roguelike.World.Providers.Saves;

public class FileProvidedWorldSource : IWorldSource
{
    private readonly string templatePath;

    public FileProvidedWorldSource(string templatePath)
    {
        this.templatePath = templatePath;
    }

    public ITileMap GetMapRepresentation()
    {
        var data = JsonSerializer.Deserialize<MapDataDto>(TitleContainer.OpenStream(templatePath));
        var map = new TileArrayMap(data.SizeX, data.SizeY);

        foreach (var actor in data.Actors)
            map[actor.X, actor.Y] = actor.Info;

        return map;
    }
}