using Roguelike.Actors;

namespace Roguelike.Components;

/// <summary>
///     Класс, отвечающий за компонент.
/// </summary>
public abstract class Component
{
    /// <summary>
    ///     Владелец компонента.
    /// </summary>
    public Actor Owner { get; private set; }

    /// <summary>
    ///     Компонент TransformComponent владельца.
    /// </summary>
    public TransformComponent Transform => Owner?.Transform;

    /// <summary>
    ///     Метод, отвечающий за инициализацию.
    /// </summary>
    public virtual void Initialize()
    {
    }

    /// <summary>
    ///     Метод, устанавливающий владельца. Может быть применён, только если владелец ещё не назначен.
    /// </summary>
    public virtual void SetOwner(Actor actor)
    {
        Owner ??= actor;
    }
}