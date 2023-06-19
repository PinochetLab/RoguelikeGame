using Roguelike.Actors;

namespace Roguelike.Commands;

public class AttackCommand : HeroCommand
{
    public AttackCommand(Hero hero) : base(hero)
    {
    }

    public override void Execute()
    {
        base.Execute();
        hero.TryAttack();
    }
}