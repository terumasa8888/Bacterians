using UnityEngine;
public interface IAttackBehaviour
{
    void Attack(IDamageable attacker, IDamageable target);
}