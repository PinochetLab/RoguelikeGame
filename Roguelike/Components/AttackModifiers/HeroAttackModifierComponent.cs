namespace Roguelike.Components.AttackModifiers;

/// <summary>
///     Данный класс отвечает за модификацию атаки игроком в зависимости от параметров игрока
/// </summary>
public class HeroAttackModifierComponent : AttackModifierComponent
{
    public override int Apply(int damage)
    {
        return (int)((damage + Owner.World.Stats.DamageModifier) * Owner.World.Stats.DamageMultiplier);
    }
}