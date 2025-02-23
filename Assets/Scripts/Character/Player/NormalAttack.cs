using UnityEngine;

/// <summary>
/// í èÌçUåÇ
/// </summary>
public class NormalAttack : IAttackBehaviour
{
    private int attackPower;
    private GameObject attacker;

    public NormalAttack(int attackPower, GameObject gameObject)
    {
        this.attackPower = attackPower;
        this.attacker = gameObject;
    }

    public void Attack(Status targetStatus)
    {
        if (targetStatus != null && attacker.tag != targetStatus.gameObject.tag)
        {
            targetStatus.TakeDamage(attackPower, attacker.tag);
        }
    }
}
