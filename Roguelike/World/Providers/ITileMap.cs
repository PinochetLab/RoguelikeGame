﻿using System.Collections.Generic;

namespace Roguelike.World.Providers;

/// <summary>
/// A container abstraction for level tile information
/// </summary>
public interface ITileMap : IEnumerable<(int x, int y, Tile tile)>
{
    /// <summary>
    /// Indexes the tile information
    /// </summary>
    /// <param name="x">The x coord</param>
    /// <param name="y">The y coord</param>
    Tile this[int x, int y] { get; set; }

    /// <summary>
    /// Gets the width of the tile information
    /// </summary>
    int Width { get; }

    /// <summary>
    /// Gets the height of the tile information
    /// </summary>
    int Height { get; }
}