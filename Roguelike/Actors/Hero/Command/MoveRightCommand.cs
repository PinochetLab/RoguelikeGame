using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Commands;
public class MoveRightCommand : HeroCommand
{

    public MoveRightCommand(Hero hero) : base(hero) { }

    public override void Execute()
    {
        base.Execute();
        hero.MoveDirection(Vector2Int.Right);
    }
}
