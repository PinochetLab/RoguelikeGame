using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Commands;

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