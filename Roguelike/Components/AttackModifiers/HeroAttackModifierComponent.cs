namespace Roguelike.Components.AttackModifiers;

public class HeroAttackModifierComponent : AttackModifierComponent
{
    public override int Apply(int damage)
    {
        return (int)((damage + Owner.World.Stats.DamageModifier) * Owner.World.Stats.DamageMultiplier);
    }
}