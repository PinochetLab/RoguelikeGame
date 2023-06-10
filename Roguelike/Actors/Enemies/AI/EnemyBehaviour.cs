using Microsoft.Xna.Framework;

namespace Roguelike.Actors.Enemies.AI;

public abstract class EnemyBehaviour
{
    public Actor actor { get; init; }
    public bool seesHero => actor.Game.World.Paths.DoesSee(actor.Transform.Position, Hero.Instance.Transform.Position);
    public bool isAttacked { get; set; }
    public abstract void Run();

    public EnemyBehaviour(Actor actor)
    {
        this.actor = actor;
    }
}