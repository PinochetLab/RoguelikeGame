using System.Collections;
using System.Collections.Generic;

namespace Roguelike.World.Providers;

internal class TileArrayMap : ITileMap
{
    private readonly Tile[,] map;

    public TileArrayMap(int width, int height)
    {
        Width = width;
        Height = height;

        map = new Tile[Width,Height];
    }

    public Tile this[int x, int y]
    {
        get => map[x, y];
        set => map[x, y] = value;
    }

    public int Width { get; }
    public int Height { get; }

    public IEnumerator<(int x, int y, Tile tile)> GetEnumerator()
    {
        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
        {
            yield return (x, y, map[x, y]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}