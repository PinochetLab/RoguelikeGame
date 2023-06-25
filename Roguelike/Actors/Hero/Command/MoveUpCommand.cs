using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Commands;

/// <summary>
///     Данный класс отвечает за команду игрока двигаться вверх
/// </summary>
public class MoveUpCommand : HeroCommand
{
    public MoveUpCommand(Hero hero) : base(hero)
    {
    }

    public override void Execute()
    {
        base.Execute();
        hero.MoveDirection(Vector2Int.Up);
    }
}