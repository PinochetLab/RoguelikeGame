using System.Collections.Generic;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

/// <summary>
///     Данный интерфейс отвечает за атаку.
/// </summary>
public interface IAttack
{
    /// <summary>
    ///     Список атакуемых атакой точек на карте относительно позиции атакующего
    /// </summary>
    public List<Vector2Int> Range { get; set; }

    /// <summary>
    ///     Метод вызываемый при атаке
    /// </summary>
    void Attack(Actor actor, Direction direction);
}