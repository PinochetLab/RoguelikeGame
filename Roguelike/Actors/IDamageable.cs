using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors;
public interface IDamageable
{
    public void TakeDamage(float damage);
}
