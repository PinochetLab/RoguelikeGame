using Roguelike.Core;

namespace Roguelike.Actors;

public interface IActorCreatable<out T> where T : Actor
{
    public static abstract T Create(BaseGame game);
}