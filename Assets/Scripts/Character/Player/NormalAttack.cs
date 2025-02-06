using UnityEngine;

/// <summary>
/// í èÌçUåÇ
/// </summary>
public class NormalAttack : MonoBehaviour, AttackBehaviour
{
    private int attackPower;
    private string cachedTag;

    void Start()
    {
        attackPower = GetComponent<Status>().Attack;
        cachedTag = gameObject.tag;
    }

    public void Attack(GameObject target)
    {
        var targetStatus = target.GetComponent<Status>();
        if (targetStatus != null && cachedTag != target.tag)
        {
            targetStatus.TakeDamage(attackPower, cachedTag);
        }
    }
}
