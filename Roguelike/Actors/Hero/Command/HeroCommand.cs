using Roguelike.Actors;

namespace Roguelike.Commands;

public class HeroCommand : ICommand
{
    protected Hero hero;

    public HeroCommand(Hero hero)
    {
        this.hero = hero;
    }

    public virtual void Execute()
    {
        hero.Game.World.TriggerOnHeroCommand();
    }
}