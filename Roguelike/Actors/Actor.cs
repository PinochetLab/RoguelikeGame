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
    public virtual string Tag { get => "none"; }

    public TransformComponent Transform;

    private readonly List<Component> components = new ();
    private readonly List<IUpdateable> updateables = new ();
    private readonly List<IDrawable> drawables = new ();

    public TComp AddComponent<TComp>() where TComp : Component, new()
    {
        var component = new TComp();

        component.SetOwner(this);

        component.Initialize();

        components.Add(component);

        var updateable = component as IUpdateable;

        if (updateable != null)
        {
            updateables.Add(updateable);
        }

        var drawable = component as IDrawable;

        if (drawable != null)
        {
            drawables.Add(drawable);
        }

        return component;
    }

    public TComp GetComponent<TComp>() where TComp : Component, new()
    {
        foreach (Component component in components)
        {
            var t = component as TComp;
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }


    public static TActor Create<TActor>(Vector2Int position) where TActor : Actor, new ()
    {
        var actor = new TActor();
        actor.Initialize(position);
        return actor;
    }

    public static TActor Create<TActor>(int x, int y) where TActor : Actor, new() => Create<TActor>(new(x, y));

    public static TActor Create<TActor>() where TActor : Actor, new() => Create<TActor>(Vector2Int.Zero);

    public static Actor CreateEmpty(Vector2Int position) => Create<Actor>(position);

    public static Actor CreateEmpty(int x, int y) => Create<Actor>(x, y);

    public static Actor CreateEmpty() => Create<Actor>();



    public virtual void Initialize(Vector2Int position) {
        Transform = AddComponent<TransformComponent>();
        Transform.Position = position;
        RoguelikeGame.AddActor(this);
        OnStart();
    }

    public virtual void OnStart() {

    }

    public virtual void Update() {
        foreach (var updateable in updateables)
        {
            updateable.Update();
        }
    }

    public virtual void Draw() {
        foreach (var drawable in drawables)
        {
            drawable.Draw();
        }
    }

    public virtual void Destroy() {
        ColliderManager.Remove(Transform.Position, this);
        RoguelikeGame.RemoveActor(this);
    }
}
