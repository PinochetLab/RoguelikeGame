using System;

namespace Roguelike.Actors.Enemies.AI;

public class AgressiveBehaviour:EnemyBehaviour
{
    private bool haveSeenHero = false;
    
    public AgressiveBehaviour(Actor actor) : base(actor)
    {
    }
    
    public override void Run()
    {
        if (haveSeenHero)
        {
            actor.Transform.Position = actor.Game.World.Paths.NextCell(actor.Transform.Position, Hero.Instance.Transform.Position);
        }
        else if (seesHero)
        {
            haveSeenHero = true;
        }
    }

}