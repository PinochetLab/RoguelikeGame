using Roguelike.Actors;

namespace Roguelike.Commands;

/// <summary>
///     Данный класс отвечает за команду игрока атаковать
/// </summary>
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