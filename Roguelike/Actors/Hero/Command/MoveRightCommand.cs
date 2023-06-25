using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Commands;

/// <summary>
///     Данный класс отвечает за команду игрока двигаться вправо
/// </summary>
public class MoveRightCommand : HeroCommand
{
    public MoveRightCommand(Hero hero) : base(hero)
    {
    }

    public override void Execute()
    {
        base.Execute();
        hero.MoveDirection(Vector2Int.Right);
    }
}