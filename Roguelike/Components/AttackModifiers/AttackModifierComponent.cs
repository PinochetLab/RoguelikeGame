namespace Roguelike.Components.AttackModifiers;

public abstract class AttackModifierComponent : Component
{
    public abstract int Apply(int damage);
}