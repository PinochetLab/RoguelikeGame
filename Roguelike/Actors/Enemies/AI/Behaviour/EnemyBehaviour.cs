namespace Roguelike.Actors.Enemies.AI.Behaviour;

/// <summary>
///     Абстрактное поведение моба
/// </summary>
public abstract class EnemyBehaviour
{
    /// <param name="actor">Моб, к которому привязано поведение</param>
    public EnemyBehaviour(Actor actor)
    {
        Actor = actor;
    }

    /// <summary>
    ///     Моб, к которому привязано поведение
    /// </summary>
    protected Actor Actor { get; }

    /// <summary>
    ///     Видит ли моб игрока
    /// </summary>
    protected bool SeesHero =>
        Actor.Game.World.Paths.DoesSee(Actor.Transform.Position, Hero.Instance.Transform.Position);

    /// <summary>
    ///     Был ли моб атакован
    /// </summary>
    public bool IsAttacked { get; set; }

    /// <summary>
    ///     Вызывается каждый ход
    /// </summary>
    public abstract void Run();
}