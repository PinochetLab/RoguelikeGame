namespace Roguelike.Actors;

/// <summary>
///     Данный интерфейс отвечает за возможность актора получать урон
/// </summary>
public interface IDamageable
{
    public void TakeDamage(int damage);
}