using Roguelike.Field;
using Roguelike.VectorUtility;
using Roguelike.Actors.InventoryUtils.Items;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input;
using Roguelike.Components.Sprites;


namespace Roguelike.Actors.InventoryUtils;

public class Inventory : CanvasActor
{
    public const string NoneName = "None";

    private const int MaxElementsCount = 5;

    public const int CellSize = 100;

    private int selected = 0;

    private static readonly List<Item> Items = Enumerable.Repeat<Item>(null, MaxElementsCount).ToList();

    private static readonly List<SpriteComponent> Cells = Enumerable.Repeat<SpriteComponent>(null, MaxElementsCount).ToList();

    private static readonly List<SpriteComponent> CellBorders = Enumerable.Repeat<SpriteComponent>(null, MaxElementsCount).ToList();

    private static readonly List<Rectangle> Rects = Enumerable.Repeat(new Rectangle(0, 0, 0, 0), MaxElementsCount).ToList();

    public static bool HasFreePlace()
    {
        return Items.Any(x => x == null);
    }

    public static void Add(Item item)
    {
        var index = Items.FindIndex(x => x == null);
        Items[index] = item;
        UpdateCell(index);
    }

    public override void OnStart()
    {
        base.OnStart();

        Items[3] = new ItemCoin();

        for (var i = 0; i < MaxElementsCount; i++)
        {
            var p = -MaxElementsCount / 2 + i;
            p = p * CellSize - CellSize / 2;

            var cellPosition = new Vector2Int(FieldInfo.ScreenWith / 2 + p, FieldInfo.ScreenHeight - CellSize);

            var size = new Vector2Int(CellSize, CellSize);

            Rects[i] = new Rectangle(cellPosition, size);

            var cellBorderActor = CreateEmpty(cellPosition);
            CellBorders[i] = cellBorderActor.AddComponent<SpriteComponent>();
            CellBorders[i].LoadTexture("Cell3");
            CellBorders[i].Size = Vector2Int.One * CellSize;
            CellBorders[i].IsTile = false;

            var cellActor = CreateEmpty(cellPosition);
            Cells[i] = cellActor.AddComponent<SpriteComponent>();
            Cells[i].Transform.Scale = Vector2.One * 0.5f;
            Cells[i].Size = Vector2Int.One * CellSize;
            Cells[i].IsTile = false;

            if (Items[i] == null) continue;

            UpdateCell(i);
        }
    }

    public static void UpdateCell(int index)
    {
        Cells[index].LoadTexture(Items[index].TextureName);
    }

    public override void Update()
    {
        var state = MouseExtended.GetState();
        for (var i = 0; i < MaxElementsCount; i++)
        {
            var rect = Rects[i];
            var cursorPosition = state.Position;
            if (cursorPosition.X >= rect.X &&
                cursorPosition.X <= rect.X + rect.Width &&
                cursorPosition.Y >= rect.Y &&
                cursorPosition.Y <= rect.Y + rect.Height)
            {
                CellBorders[selected].LoadTexture("Cell3");
                CellBorders[i].LoadTexture("SelectedCell");
                selected = i;
                return;
            }
        }
        CellBorders[selected].LoadTexture("Cell3");
    }
}
