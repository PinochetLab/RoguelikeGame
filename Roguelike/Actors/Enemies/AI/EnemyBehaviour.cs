namespace Roguelike.Actors.Enemies.AI;

public abstract class EnemyBehaviour
{
    public EnemyBehaviour(Actor actor)
    {
        Actor = actor;
    }

    public Actor Actor { get; init; }
    public bool SeesHero => Actor.Game.World.Paths.DoesSee(Actor.Transform.Position, Hero.Instance.Transform.Position);
    public bool IsAttacked { get; set; }
    public abstract void Run();
}