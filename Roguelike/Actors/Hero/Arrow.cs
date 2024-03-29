﻿using System;
using System.Collections.Generic;
using Roguelike.Components;
using Roguelike.Components.Colliders;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors;

/// <summary>
///     Данный класс отвечает за стрелу, которой стреляет игрок
/// </summary>
public class Arrow : Actor, IActorCreatable<Arrow>, ICloneable
{
    private ColliderComponent collider;

    private int damage;
    private DamagerComponent damagerComponent;
    private SpriteComponent spriteComponent;

    public Arrow(BaseGame game) : base(game)
    {
    }

    public override string Tag => Tags.ArrowTag;

    /// <summary>
    ///     Летит ли стрела
    /// </summary>
    public bool IsMoving { get; set; } = true;

    /// <summary>
    ///     Урон наносимый стрелой при касании
    /// </summary>
    public int Damage
    {
        get => damage;
        set
        {
            damage = value;
            damagerComponent.Damages[Vector2Int.Zero] = damage;
        }
    }

    public static Arrow Create(BaseGame game)
    {
        return new Arrow(game);
    }

    public object Clone()
    {
        var clone = Game.World.CreateActor<Arrow>();
        clone.Damage = Damage;
        return clone;
    }

    public override void Initialize()
    {
        base.Initialize();
        World.onHeroCommand += Move;

        spriteComponent = AddComponent<SpriteComponent>();
        spriteComponent.SetTexture("Arrow");

        damagerComponent = AddComponent<DamagerComponent>();
        damagerComponent.Damages = new Dictionary<Vector2Int, int> { { Vector2Int.Zero, Damage } };
        World.onHeroCommand += damagerComponent.Damage;

        collider = AddComponent<ColliderComponent>();
        collider.Type = ColliderType.Trigger;
        collider.OnTriggerEnter += OnTriggerEnter;
    }

    /// <summary>
    ///     Создает за пределами экрана неподвижную стрелу с заданным уроном
    /// </summary>
    public static Arrow GetPrototype(BaseGame game, int damage)
    {
        var prototype = game.World.CreateActor<Arrow>();
        prototype.Transform.Position = new Vector2Int(-1, -1);
        prototype.IsMoving = false;
        prototype.Damage = damage;
        return prototype;
    }

    private void Move()
    {
        if (!IsMoving) return;
        Transform.Position += Transform.Direction;
    }

    protected override void Dispose(bool isDisposing)
    {
        World.onHeroCommand -= damagerComponent.Damage;
        base.Dispose(isDisposing);
    }

    private void OnTriggerEnter(ColliderComponent other)
    {
        if (other.Type == ColliderType.Solid) Dispose();
    }
}