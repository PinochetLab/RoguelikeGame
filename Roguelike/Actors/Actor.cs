using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Roguelike.Components;
using Roguelike.Core;
using IUpdateable = Roguelike.Core.IUpdateable;

namespace Roguelike.Actors;

/// <summary>
///     Класс Actor - это класс игрового объекта.
/// </summary>
public class Actor : DrawableGameComponent
{
    private readonly List<Component> components = new();

    private readonly List<DrawableComponent> drawables = new();

    private readonly List<IUpdateable> updatable = new();

    public Actor(BaseGame game) : base(game)
    {
        Game = game;
        World = game.World;
    }

    /// <summary>
    ///     Tag используется для того, чтобы различать различные игровые объекты без кастов.
    /// </summary>
    public virtual string Tag => "";

    /// <summary>
    ///     Компонент TransformComponent существует у каждого игрового объекта
    ///     для удобства получения информации о позиции/размере и т.д.
    /// </summary>
    public TransformComponent Transform { get; protected set; }

    /// <summary>
    ///     Текущая игра, к которой принадлежит Actor
    /// </summary>
    public new BaseGame Game { get; }

    /// <summary>
    ///     Игровой мир, в котором создан Actor
    /// </summary>
    public BaseWorldComponent World { get; }

    /// <summary>
    ///     Данная функция создаёт у игрового объекта компонент необходимого типа, устанавливает текущий игровой
    ///     объект его владельцем, вызывает у компонента метод Initialize и возвращает созданный компонент.
    /// </summary>
    public TComponent AddComponent<TComponent>() where TComponent : Component, new()
    {
        var component = new TComponent();

        component.SetOwner(this);

        component.Initialize();

        if (component is DrawableComponent draw)
            Game.AddDrawable(draw);

        components.Add(component);

        switch (component)
        {
            case IUpdateable updateable:
                updatable.Add(updateable);
                break;
            case DrawableComponent drawable:
                drawables.Add(drawable);
                break;
        }

        return component;
    }

    /// <summary>
    ///     Данная функция возвращает первый найденный компонент необходимого типа.
    /// </summary>
    public TComp GetComponent<TComp>() where TComp : Component, new()
    {
        foreach (var component in components)
            if (component is TComp t)
                return t;

        return null;
    }

    /// <summary>
    ///     Данный метод вызывает инициализацию игрового объекта, создаёт компонент TransformComponent
    ///     и добавляет текущий игровой компонент в глобальный список игровых объектов.
    /// </summary>
    public virtual void Initialize(Vector2Int position)
    {
        Transform = AddComponent<TransformComponent>();
        Transform.Position = position;
        Initialize();
    }

    /// <summary>
    ///     Данный виртуальный метод вызывается каждый кадр игровой логики.
    ///     Внутри этого метода вызывается метов Update у всех компонентов объекта, которые реализуют интерфейс IUpdateable.
    /// </summary>
    public override void Update(GameTime time)
    {
        foreach (var updateable in updatable)
            updateable.Update(time.GetElapsedSeconds());
    }

    protected override void Dispose(bool isDisposing)
    {
        Game.World.RemoveActor(this);
        Transform.Children.ForEach(x => x.Owner.Dispose());
        drawables.ForEach(Game.RemoveDrawable);
    }
}