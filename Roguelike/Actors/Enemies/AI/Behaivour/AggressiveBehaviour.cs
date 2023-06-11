namespace Roguelike.Actors.Enemies.AI.Behaivour;

public class AggressiveBehaviour : EnemyBehaviour
{
    private bool haveSeenHero = false;

    public AggressiveBehaviour(Actor actor) : base(actor)
    {
    }

    public override void Run()
    {
        if (haveSeenHero)
        {
            Actor.Transform.Position =
                Actor.Game.World.Paths.NextCell(Actor.Transform.Position, Hero.Instance.Transform.Position);
        }
        else if (SeesHero)
        {
            haveSeenHero = true;
        }
    }
}