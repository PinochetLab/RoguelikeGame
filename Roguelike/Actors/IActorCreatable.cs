using Roguelike.Core;

namespace Roguelike.Actors;

/// <summary>
///     Данный интерфейс отвечает за возможность актора быть созданным в мире
/// </summary>
public interface IActorCreatable<out T> where T : Actor
{
    public static abstract T Create(BaseGame game);
}