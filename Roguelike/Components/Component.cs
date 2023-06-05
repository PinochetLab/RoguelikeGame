using Roguelike.Actors;

namespace Roguelike.Components;

public abstract class Component
{
    private Actor owner = null;

    public Actor Owner => owner;

    public TransformComponent Transform => Owner?.Transform;

    public virtual void Initialize() { }

    public virtual void SetOwner(Actor actor)
    {
        owner ??= actor;
    }
}
