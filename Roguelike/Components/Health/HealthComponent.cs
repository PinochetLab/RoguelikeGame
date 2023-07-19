using System;

namespace Roguelike.Components;

public class HealthComponent : Component
{
    private int health = 100;

    private int maxHealth = 100;

    public float HealthRatio => (float)health / maxHealth;

    public int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
                OnDeath?.Invoke();
            }

            OnHealthChange?.Invoke();
        }
    }

    public event Action OnDeath;
    public event Action OnHealthChange;

    public override void Initialize()
    {
        base.Initialize();

        OnHealthChange?.Invoke();
    }

    public void SetMaxHealth(int maxHealth, bool updateHealth = false)
    {
        this.maxHealth = maxHealth;
        if (updateHealth) health = maxHealth;
        OnHealthChange?.Invoke();
    }
}