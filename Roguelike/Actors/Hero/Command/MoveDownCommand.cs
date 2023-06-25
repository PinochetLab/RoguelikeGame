using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Commands;

/// <summary>
///     Данный класс отвечает за команду игрока двигаться вниз
/// </summary>
public class MoveDownCommand : HeroCommand
{
    public MoveDownCommand(Hero hero) : base(hero)
    {
    }

    public override void Execute()
    {
        base.Execute();
        hero.MoveDirection(Vector2Int.Down);
    }
}