using UnityEngine;

/// <summary>
/// ぶつかった敵とともに爆発
/// </summary>
public class Explode : MonoBehaviour, AttackBehaviour
{
    [SerializeField] private GameObject explosionEffect;

    public void Attack(GameObject target)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(target);
        Destroy(gameObject);
    }
}
