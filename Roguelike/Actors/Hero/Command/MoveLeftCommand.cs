﻿using Roguelike.Actors;
using Roguelike.Core;

namespace Roguelike.Commands;

public class MoveLeftCommand : HeroCommand
{
    public MoveLeftCommand(Hero hero) : base(hero)
    {
    }

    public override void Execute()
    {
        base.Execute();
        hero.MoveDirection(Vector2Int.Left);
    }
}