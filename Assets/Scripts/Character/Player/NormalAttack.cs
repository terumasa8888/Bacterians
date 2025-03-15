using UnityEngine;

/// <summary>
/// �ʏ�U��
/// </summary>
public class NormalAttack : MonoBehaviour, IAttackBehaviour
{
    public void Attack(GameObject attacker, DamageableBase target)
    {
        if (target != null && attacker.CompareTag(target.GameObject.tag) == false)
        {

            target.TakeDamage(attacker);
        }
    }
}
