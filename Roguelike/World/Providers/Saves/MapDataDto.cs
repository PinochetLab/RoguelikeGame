using System.Collections.Generic;

namespace Roguelike.World.Providers.Saves;

public record MapDataDto(int SizeX, int SizeY, IEnumerable<MapElementDto> Actors);