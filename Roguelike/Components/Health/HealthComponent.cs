using System;

namespace Roguelike.Components;

public class HealthComponent : Component
{
    private float health = 100;

    private float maxHealth = 100;

    public float HealthRatio => health / maxHealth;

    public float Health
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

    public void SetMaxHealth(float maxHealth, bool updateHealth = false)
    {
        this.maxHealth = maxHealth;
        if (updateHealth) health = maxHealth;
    }
}