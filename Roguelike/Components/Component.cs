using Roguelike.Actors;

namespace Roguelike.Components;

public abstract class Component
{
    public Actor Owner { get; private set; }

    public TransformComponent Transform => Owner?.Transform;

    public virtual void Initialize() { }

    public virtual void SetOwner(Actor actor)
    {
        Owner ??= actor;
    }
}
