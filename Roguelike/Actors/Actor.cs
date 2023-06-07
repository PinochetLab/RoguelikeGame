using Roguelike.Components;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Roguelike.Core;
using IDrawable = Roguelike.Core.IDrawable;
using IUpdateable = Roguelike.Core.IUpdateable;

namespace Roguelike.Actors;

/// <summary>
///  Класс Actor - это класс игрового объекта.
/// </summary>
public class Actor
{
    /// <summary>
    ///  Tag используется для того, чтобы различать различные игровые объекты без кастов.
    /// </summary>
    public virtual string Tag => "none";

    /// <summary>
    ///  Компонент TransformComponent существует у каждого игрового объекта для удобства получения информации о позиции/размере и т.д.
    /// </summary>
    public TransformComponent Transform { get; protected set; }

    private readonly List<Component> components = new();

    private readonly List<IUpdateable> updatable = new();

    private readonly List<IDrawable> drawables = new();

    /// <summary>
    ///  Данная функция создаёт у игрового объекта компонент необходимого типа, устанавливает текущий игровой 
    ///  объект его владельцем, вызывает у компонента метод Initialize и возвращает созданный компонент.
    /// </summary>
    public TComp AddComponent<TComp>() where TComp : Component, new()
    {
        var component = new TComp();

        component.SetOwner(this);

        component.Initialize();

        if (component is IDrawable draw)
            RoguelikeGame.AddDrawable(draw);

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

    /// <summary>
    ///  Данная функция возвращает первый найденный компонент необходимого типа.
    /// </summary>
    public TComp GetComponent<TComp>() where TComp : Component, new()
    {
        foreach (var component in components)
            if (component is TComp t)
                return t;

        return null;
    }

    /*
     * Данная функция создаёт игровой объект переданного типа, присваивая его позицию к position, после чего возвращает его.
    */
    public static TActor Create<TActor>(Vector2Int position) where TActor : Actor, new()
    {
        var actor = new TActor();
        actor.Initialize(position);
        return actor;
    }

    /// <summary>
    /// Данная функция создаёт игровой объект переданного типа,
    /// присваивая его позицию к Vector2Int(x, y), после чего возвращает его.
    /// </summary>
    public static TActor Create<TActor>(int x, int y) where TActor : Actor, new()
        => Create<TActor>(new Vector2Int(x, y));

    /// <summary>
    /// Данная функция создаёт игровой объект переданного типа в начале координат, после чего возвращает его.
    /// </summary>
    public static TActor Create<TActor>() where TActor : Actor, new() => Create<TActor>(Vector2Int.Zero);

    /// <summary>
    /// Данная функция создаёт пустой игровой объект, присваивая его позицию к position, после чего возвращает его.
    /// </summary>
    public static Actor CreateEmpty(Vector2Int position) => Create<Actor>(position);

    /// <summary>
    /// Данная функция создаёт пустой игровой объект, присваивая его позицию к Vector2Int(x, y), после чего возвращает его.
    /// </summary>
    public static Actor CreateEmpty(int x, int y) => Create<Actor>(x, y);

    /// <summary>
    /// Данная функция создаёт пустой игровой объект в начале координат, после чего возвращает его.
    /// </summary>
    public static Actor CreateEmpty() => Create<Actor>();

    /// <summary>
    /// Данный метод вызывает инициализацию игрового объекта, создаёт компонент TransformComponent
    /// и добавляет текущий игровой компонент в глобальный список игровых объектов.
    /// </summary>
    public virtual void Initialize(Vector2Int position)
    {
        Transform = AddComponent<TransformComponent>();
        Transform.Position = position;
        RoguelikeGame.AddActor(this);
        OnStart();
    }

    /// <summary>
    /// Данный виртуальный метод вызывается сразу после инициализации игрового обьекта.
    /// </summary>
    public virtual void OnStart()
    {
    }

    /// <summary>Данный виртуальный метод вызывается каждый кадр игровой логики.
    /// Внутри этого метода вызывается метов Update у всех компонентов объекта, которые реализуют интерфейс IUpdateable.
    /// </summary>
    public virtual void Update(float deltaTime)
    {
        foreach (var updateable in updatable)
            updateable.Update(deltaTime);
    }

    /// <summary>
    /// Данный виртуальный метод вызывается при удалении объекта.
    /// </summary>
    public virtual void Destroy()
    {
        ColliderManager.Remove(Transform.Position, this);
        RoguelikeGame.RemoveActor(this);
        drawables.ForEach(RoguelikeGame.RemoveDrawable);
    }
}