using Roguelike.Field;
using Roguelike.Actors.InventoryUtils.Items;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input;
using Roguelike.Components.Sprites;
using Roguelike.Core;
using Roguelike.World;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Roguelike.Actors.InventoryUtils;

/// <summary>
/// Данный класс - класс инвенторя.
/// </summary>
public class Inventory : BaseGameSystem
{
    /// <summary>
    /// Строка, отвечающая за отсутствие предмета.
    /// </summary>
    public const string NoneName = "None";

    private const int MaxElementsCount = 7;

    /// <summary>
    /// Размер клетки инвенторя в пикселях.
    /// </summary>
    public const int CellSize = 100;

    private int selected = 0;

    private static readonly List<Item> Items = Enumerable.Repeat<Item>(null, MaxElementsCount).ToList();

    private static readonly List<SpriteComponent> Cells = Enumerable.Repeat<SpriteComponent>(null, MaxElementsCount).ToList();

    private static readonly List<SpriteComponent> CellBorders = Enumerable.Repeat<SpriteComponent>(null, MaxElementsCount).ToList();

    private static readonly List<Rectangle> Rects = Enumerable.Repeat(new Rectangle(0, 0, 0, 0), MaxElementsCount).ToList();

    /// <summary>
    /// Данный метод возвращет true, если в инвентаре есть место, и false в противном случае.
    /// </summary>
    public static bool HasFreePlace()
    {
        return Items.Any(x => x == null);
    }

    /// <summary>
    /// Данный метод добавляет предмет в инвентарь.
    /// </summary>
    public static void Add(Item item)
    {
        var index = Items.FindIndex(x => x == null);
        Items[index] = item;
        UpdateCell(index);
    }

    public Inventory(BaseGame game) : base(game)
    { }

    public override void Initialize()
    {
        base.Initialize();

        Items[3] = new ItemCoin();

        Items[0] = new Sword1Item();
        Items[1] = new Bow1Item();

        for (var i = 0; i < MaxElementsCount; i++)
        {
            var p = -MaxElementsCount / 2 + i;
            p = p * CellSize;

            var cellPosition = new Vector2Int(FieldInfo.ScreenWith / 2 + p, FieldInfo.ScreenHeight - CellSize / 2);

            var size = new Vector2Int(CellSize, CellSize);

            Rects[i] = new Rectangle(cellPosition, size);

            var cellBorderActor = Game.World.CreateActor(cellPosition);
            CellBorders[i] = cellBorderActor.AddComponent<SpriteComponent>();
            CellBorders[i].SetTexture("Cell3");
            CellBorders[i].Size = Vector2Int.One * CellSize;
            CellBorders[i].Transform.IsTile = false;
            CellBorders[i].Canvas = true;
            CellBorders[i].DrawOrder = 0;

            var cellActor = Game.World.CreateActor(cellPosition);
            Cells[i] = cellActor.AddComponent<SpriteComponent>();
            Cells[i].Transform.Scale = Vector2.One * 0.5f;
            Cells[i].Size = Vector2Int.One * CellSize;
            Cells[i].Transform.IsTile = false;
            Cells[i].Canvas = true;
            Cells[i].DrawOrder = 1;

            if (Items[i] == null) continue;

            UpdateCell(i);
        }
        SelectCell(selected);
    }

    private static void UpdateCell(int index)
    {
        Cells[index].SetTexture(Items[index].TextureName);
    }

    private void SelectCell(int i)
    {
        CellBorders[selected].SetTexture("Cell3");
        CellBorders[i].SetTexture("SelectedCell");
        selected = i;
        Hero.Instance.Item = Items[selected];
    }

    public override void Update(GameTime deltaTime)
    {
        var state = MouseExtended.GetState();
        int next = selected + state.DeltaScrollWheelValue / 120;
        if (next == selected) return;
        if (next >= MaxElementsCount) next = 0;
        else if (next < 0) next = MaxElementsCount - 1;
        SelectCell(next);
    }
}
