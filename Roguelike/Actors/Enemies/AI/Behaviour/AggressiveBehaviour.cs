namespace Roguelike.Actors.Enemies.AI.Behaviour;

public class AggressiveBehaviour : EnemyBehaviour
{
    private bool haveSeenHero;

    public AggressiveBehaviour(Actor actor) : base(actor)
    {
    }

    public override void Run()
    {
        if (haveSeenHero)
            Actor.Transform.Position =
                Actor.Game.World.Paths.NextCell(Actor.Transform.Position, Hero.Instance.Transform.Position);
        else if (SeesHero) haveSeenHero = true;
    }
}