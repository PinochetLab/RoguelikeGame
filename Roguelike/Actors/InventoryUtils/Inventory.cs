using Roguelike.Field;
using Roguelike.VectorUtility;
using Roguelike.Actors.InventoryUtils.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace Roguelike.Actors.InventoryUtils
{
    public class Inventory : CanvasActor
    {
        public const string NoneName = "None";
        private const int MaxElementsCount = 5;
        public const int CellSize = 100;

        private int selected = 0;

        private static List<Item> items = Enumerable.Repeat<Item>(null, MaxElementsCount).ToList();
        private static List<Image> cells = Enumerable.Repeat<Image>(null, MaxElementsCount).ToList();
        private static List<Image> cellBorders = Enumerable.Repeat<Image>(null, MaxElementsCount).ToList();
        private static List<Rectangle> rects = Enumerable.Repeat(new Rectangle(0, 0, 0, 0), MaxElementsCount).ToList();

        private static Inventory instance;
        public Inventory(Vector2Int position) : base(position)
        {
            instance = this;
        }

        public static bool HasFreePlace()
        {
            return items.Any(x => x == null);
        }

        public static void Add(Item item)
        {
            int index = items.FindIndex(x => x == null);
            items[index] = item;
            UpdateCell(index);
        }

        public override void OnStart()
        {
            base.OnStart();

            items[3] = new ItemCoin();

            for (int i = 0; i < MaxElementsCount; i++)
            {
                int p = -MaxElementsCount / 2 + i;
                p = p * CellSize - CellSize / 2;

                Vector2Int cellPosition = new Vector2Int(FieldInfo.ScreenWith / 2 + p, FieldInfo.ScreenHeight - CellSize);

                Vector2Int size = new Vector2Int(CellSize, CellSize);

                rects[i] = new Rectangle(cellPosition, size);

                cellBorders[i] = new Image(cellPosition, size);
                cellBorders[i].LoadTexture("Cell3");

                cells[i] = new Image(cellPosition, (Vector2)size);
                cells[i].Transform.Scale = Vector2.One * 0.5f;

                if (items[i] == null) continue;

                UpdateCell(i);
            }
        }

        public static void UpdateCell(int index)
        {
            cells[index].LoadTexture(items[index].TextureName);
        }

        public override void Update(float deltaTime)
        {
            var state = MouseExtended.GetState();
            for (var i = 0; i < MaxElementsCount; i++)
            {
                var rect = rects[i];
                var cursorPosition = state.Position;
                if (cursorPosition.X >= rect.X &&
                    cursorPosition.X <= rect.X + rect.Width &&
                    cursorPosition.Y >= rect.Y &&
                    cursorPosition.Y <= rect.Y + rect.Height)
                {
                    cellBorders[selected].LoadTexture("Cell3");
                    cellBorders[i].LoadTexture("SelectedCell");
                    selected = i;
                    return;
                }
            }
            cellBorders[selected].LoadTexture("Cell3");
        }
    }
}
