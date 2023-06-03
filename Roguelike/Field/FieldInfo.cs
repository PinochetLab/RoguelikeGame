using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Field {
    public static class FieldInfo {
        public static int PixelSize { get; set; } = 900;

        public static int CellCount { get; set; } = 15;

        public static int CellSize => PixelSize / CellCount;
    }
}
