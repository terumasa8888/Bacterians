using UnityEngine;
public interface IAttackBehaviour
{
    void Attack(GameObject attacker, DamageableBase target);
}