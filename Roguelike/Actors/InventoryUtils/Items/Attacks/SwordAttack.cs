using System.Collections.Generic;
using System.Threading.Tasks;
using Roguelike.Core;

namespace Roguelike.Actors.InventoryUtils.Items.Attacks;

public class SwordAttack : IMeleeAttack
{
    public int Damage { get; set; }
    public List<Vector2Int> range { get; set; } = new();

    public void Atack(Actor actor, Direction direction)
    {
        Task.Run(() => Animation(actor));
        foreach (var attack in range)
        foreach (var damageable in actor.World.Colliders.FindAll<IDamageable>(actor.Transform.Position +
                                                                              attack.Rotate(direction)))
            damageable.TakeDamage(Damage);
    }

    public async void Animation(Actor actor)
    {
        for (var i = 0; i < 2; i++)
        {
            Hero.Instance.weaponSlot.Offset = Hero.Instance.CurrentDirection * 30;
            await Task.Delay(50);
            Hero.Instance.weaponSlot.Offset = Vector2Int.Zero;
            await Task.Delay(50);
        }
    }
}