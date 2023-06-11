namespace Roguelike.Actors.Enemies.AI.Behaivour;

public class LazyBehaviour : EnemyBehaviour
{
    public const int Cooldown = 3;
    private int rage;

    public LazyBehaviour(Actor actor) : base(actor)
    {
    }

    public int Rage
    {
        get => rage;
        set
        {
            if (value is >= 0 and <= Cooldown) rage = value;
        }
    }


    public override void Run()
    {
        if (!IsAttacked) return;

        if (SeesHero)
        {
            Actor.Transform.Position =
                Actor.Game.World.Paths.NextCell(Actor.Transform.Position, Hero.Instance.Transform.Position);
            Rage++;
        }
        else
        {
            Rage--;
        }

        if (Rage == 0)
        {
            IsAttacked = false;
            Rage = Cooldown;
        }
    }
}