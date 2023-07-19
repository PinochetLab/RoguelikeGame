using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Roguelike.Actors;
using Roguelike.Actors.Enemies.AI.Behaviour;
using Roguelike.Commands;
using Roguelike.Components.Colliders;

namespace Roguelike.Core;

public abstract class BaseWorldComponent : BaseGameSystem
{
    private readonly List<Actor> actors = new();
    private readonly List<Actor> actorsToAdd = new();
    private readonly List<Actor> actorsToRemove = new();

    protected BaseWorldComponent(BaseGame game) : base(game)
    {
        Paths = new PathFinder(Game);
        Stats = new StatsManager(Game);
    }

    public ColliderManager Colliders { get; protected set; } = new();
    public PathFinder Paths { get; protected set; }
    public StatsManager Stats { get; protected set; }

    public CommandInvoker Commands { get; protected set; }

    public Hero Hero { get; protected set; }

    public event Action onHeroCommand;

    public override void Initialize()
    {
        base.Initialize();
        Stats.Initialize();
    }

    /// <summary>
    ///     Данная функция создаёт игровой объект переданного типа,
    ///     присваивая его позицию к position, после чего возвращает его.
    /// </summary>
    /// <param name="position">Позиция объекта на сцене</param>
    /// <typeparam name="TActor">Тип объекта</typeparam>
    /// <returns>Инициализированный объект на сцене</returns>
    public TActor CreateActor<TActor>(Vector2Int position)
        where TActor : Actor, IActorCreatable<TActor>
    {
        var actor = TActor.Create(Game);
        actor.Spawn(position);
        actorsToAdd.Add(actor);
        return actor;
    }

    /// <summary>
    ///     Данная функция создаёт игровой объект переданного типа,
    ///     присваивая его позицию к (x, y), после чего возвращает его.
    /// </summary>
    /// <param name="x">Позиция объекта на сцене по координате X</param>
    /// <param name="y">Позиция объекта на сцене по координате Y</param>
    /// <typeparam name="TActor">Тип объекта</typeparam>
    /// <returns>Инициализированный объект на сцене</returns>
    public TActor CreateActor<TActor>(int x, int y) where TActor : Actor, IActorCreatable<TActor>
    {
        return CreateActor<TActor>(new Vector2Int(x, y));
    }

    /// <summary>
    ///     Данная функция создаёт игровой объект переданного типа,
    ///     присваивая его позицию к (0, 0), после чего возвращает его.
    /// </summary>
    /// <typeparam name="TActor">Тип объекта</typeparam>
    /// <returns>Инициализированный объект на сцене</returns>
    public TActor CreateActor<TActor>() where TActor : Actor, IActorCreatable<TActor>
    {
        return CreateActor<TActor>(Vector2Int.Zero);
    }

    /// <summary>
    ///     Данная функция создаёт игровой объект переданного типа,
    ///     присваивая его позицию к position, после чего возвращает его.
    /// </summary>
    /// <param name="position">Позиция объекта на сцене</param>
    /// <returns>Инициализированный объект на сцене</returns>
    public Actor CreateActor(Vector2Int position)
    {
        var actor = new Actor(Game);
        actor.Spawn(position);
        actorsToAdd.Add(actor);
        return actor;
    }

    /// <summary>
    ///     Данная функция создаёт игровой объект переданного типа,
    ///     присваивая его позицию к (x, y), после чего возвращает его.
    /// </summary>
    /// <param name="x">Позиция объекта на сцене по координате X</param>
    /// <param name="y">Позиция объекта на сцене по координате Y</param>
    /// <returns>Инициализированный объект на сцене</returns>
    public Actor CreateActor(int x, int y)
    {
        return CreateActor(new Vector2Int(x, y));
    }

    /// <summary>
    ///     Данная функция создаёт игровой объект переданного типа,
    ///     присваивая его позицию к (0, 0), после чего возвращает его.
    /// </summary>
    /// <returns>Инициализированный объект на сцене</returns>
    public Actor CreateActor()
    {
        return CreateActor(Vector2Int.Zero);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        foreach (var actor in actors)
            actor.Update(gameTime);

        Colliders.Update(gameTime.GetElapsedSeconds());

        UpdateStatesOfActors();
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        foreach (var actor in actors)
            actor.Draw(gameTime);
    }

    public void RemoveActor(Actor actor)
    {
        actorsToRemove.Add(actor);
    }

    protected override void Dispose(bool disposing)
    {
        actors.ForEach(x => x.Dispose());
        base.Dispose(disposing);
    }

    private void UpdateStatesOfActors()
    {
        actorsToRemove.ForEach(x =>
        {
            actors.Remove(x);
            Colliders.Remove(x.Transform.Position, x);
        });
        actorsToRemove.Clear();

        actorsToAdd.ForEach(x => actors.Add(x));
        actorsToAdd.Clear();
    }

    public void TriggerOnHeroCommand()
    {
        onHeroCommand?.Invoke();
    }
}