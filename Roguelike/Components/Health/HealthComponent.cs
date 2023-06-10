using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Components;
public class HealthComponent : Component
{
    public event Action OnDeath;
    public event Action OnHealthChange;

    private int health = 100;

    private int maxHealth = 100;

    public override void Initialize()
    {
        base.Initialize();

        OnHealthChange?.Invoke();
    }

    public void SetMaxHealth(int maxHealth, bool updateHealth = false)
    {
        this.maxHealth = maxHealth;
        if (updateHealth)
        {
            health = maxHealth;
        }
        OnHealthChange?.Invoke();
    }

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
}
