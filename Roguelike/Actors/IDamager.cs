using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Roguelike.Core;

namespace Roguelike.Actors;

public interface IDamager
{
    Dictionary<Vector2Int, int> damages { get; set; }
}