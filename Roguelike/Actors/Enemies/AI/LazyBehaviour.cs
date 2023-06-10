using System;

namespace Roguelike.Actors.Enemies.AI;

public class LazyBehaviour:EnemyBehaviour
{
    public LazyBehaviour(Actor actor) : base(actor)
    {
    }

    public override void Run()
    {
        throw new NotImplementedException();
    }
}