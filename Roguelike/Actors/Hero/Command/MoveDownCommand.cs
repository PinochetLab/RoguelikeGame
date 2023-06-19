using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Commands;

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