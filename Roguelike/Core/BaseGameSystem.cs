using Microsoft.Xna.Framework;

namespace Roguelike.Core;

public abstract class BaseGameSystem : DrawableGameComponent
{
    public new BaseGame Game { get; private set; }

    protected BaseGameSystem(BaseGame game) : base(game)
    {
        Game = game;
    }
}