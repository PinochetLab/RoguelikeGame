using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Components;
public class HealthComponent : Component
{
    public Action OnDeath { get; set; }
    public Action OnHealthChange { get; set; }

    private float health = 100;

    private float maxHealth = 100;

    public void SetMaxHealth(float maxHealth, bool updateHealth = false)
    {
        this.maxHealth = maxHealth;
        if (updateHealth)
        {
            health = maxHealth;
        }
    }

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
                OnDeath();
            }
            OnHealthChange();
        }
    }
}
