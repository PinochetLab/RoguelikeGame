using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.VectorUtility;
using Roguelike.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors;

public class Actor
{
    public virtual string Tag => "none";

    public TransformComponent Transform;

    private readonly List<Component> components = new();
    private readonly List<IUpdateable> updatable = new();
    private readonly List<IDrawable> drawables = new();

    public TComp AddComponent<TComp>() where TComp : Component, new()
    {
        var component = new TComp();

        component.SetOwner(this);

        component.Initialize();

        components.Add(component);

        switch (component)
        {
            case IUpdateable updateable:
                updatable.Add(updateable);
                break;
            case IDrawable drawable:
                drawables.Add(drawable);
                break;
        }

        return component;
    }

    public TComp GetComponent<TComp>() where TComp : Component, new()
    {
        foreach (var component in components)
            if (component is TComp t)
                return t;

        return null;
    }


    public static TActor Create<TActor>(Vector2Int position) where TActor : Actor, new()
    {
        var actor = new TActor();
        actor.Initialize(position);
        return actor;
    }

    public static TActor Create<TActor>(int x, int y) where TActor : Actor, new()
        => Create<TActor>(new Vector2Int(x, y));

    public static TActor Create<TActor>() where TActor : Actor, new() => Create<TActor>(Vector2Int.Zero);

    public static Actor CreateEmpty(Vector2Int position) => Create<Actor>(position);

    public static Actor CreateEmpty(int x, int y) => Create<Actor>(x, y);

    public static Actor CreateEmpty() => Create<Actor>();


    public virtual void Initialize(Vector2Int position)
    {
        Transform = AddComponent<TransformComponent>();
        Transform.Position = position;
        RoguelikeGame.AddActor(this);
        OnStart();
    }

    public virtual void OnStart()
    {
    }

    public virtual void Update(float deltaTime)
    {
        foreach (var updateable in updatable)
            updateable.Update(deltaTime);
    }

    public virtual void Draw(float delta)
    {
        foreach (var drawable in drawables)
            drawable.Draw(delta);
    }

    public virtual void Destroy()
    {
        ColliderManager.Remove(Transform.Position, this);
        RoguelikeGame.RemoveActor(this);
    }
}