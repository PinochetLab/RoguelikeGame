using Microsoft.Xna.Framework;

namespace Roguelike.Core;

public abstract class BaseGameSystem : DrawableGameComponent
{
    protected BaseGameSystem(BaseGame game) : base(game)
    {
        Game = game;
    }

    public new BaseGame Game { get; }
}