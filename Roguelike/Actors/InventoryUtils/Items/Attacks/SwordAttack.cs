using System.Collections.Generic;
using System.Threading.Tasks;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

public class SwordAttack : IMeleeAttack
{
    public int Damage { get; set; }
    public List<Vector2Int> range { get; set; } = new();

    public void Attack(Actor actor, Direction direction)
    {
        Task.Run(Animation);
        foreach (var attack in range)
        foreach (var damageable in actor.World.Colliders.FindAll<IDamageable>(actor.Transform.Position +
                                                                              attack.Rotate(direction)))
            if (damageable != actor)
                damageable.TakeDamage(Damage);
    }

    private static async void Animation()
    {
        for (var i = 0; i < 2; i++)
        {
            Hero.Instance.WeaponSlot.Offset = Hero.Instance.CurrentDirection * 30;
            await Task.Delay(50);
            Hero.Instance.WeaponSlot.Offset = Vector2Int.Zero;
            await Task.Delay(50);
        }
    }
}