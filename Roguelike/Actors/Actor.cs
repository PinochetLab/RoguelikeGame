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
public class Actor : DrawableGameComponent
{
    /// <summary>
    ///  Tag используется для того, чтобы различать различные игровые объекты без кастов.
    /// </summary>
    public virtual string Tag => "none";

    /// <summary>
    ///  Компонент TransformComponent существует у каждого игрового объекта для удобства получения информации о позиции/размере и т.д.
    /// </summary>
    public TransformComponent Transform { get; protected set; }

    public new BaseGame Game { get; private set; }

    public BaseWorldComponent World { get; private set; }

    private readonly List<Component> components = new();

    private readonly List<IUpdateable> updatable = new();

    private readonly List<IDrawable> drawables = new();

    public Actor(BaseGame game) : base(game)
    {
        Game = game;
        World = game.World;
    }

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
            Game.AddDrawable(draw);

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

    /// <summary>
    /// Данный метод вызывает инициализацию игрового объекта, создаёт компонент TransformComponent
    /// и добавляет текущий игровой компонент в глобальный список игровых объектов.
    /// </summary>
    public virtual void Initialize(Vector2Int position)
    {
        Transform = AddComponent<TransformComponent>();
        Transform.Position = position;
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
    public override void Update(GameTime time)
    {
        foreach (var updateable in updatable)
            updateable.Update(time.GetElapsedSeconds());
    }

    /// <summary>
    /// Метод удаляет вызывает удаление Actor из текущей игры
    /// </summary>
    public virtual void Destroy()
    {
        Game.World.RemoveActor(this);
        drawables.ForEach(Game.RemoveDrawable);
    }
}