namespace Roguelike.Components.AttackModifiers;

/// <summary>
///     Данный класс отвечает за абстрактную модификацию атаки
/// </summary>
public abstract class AttackModifierComponent : Component
{
    public abstract int Apply(int damage);
}