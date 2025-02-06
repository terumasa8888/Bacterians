using UnityEngine;

/// <summary>
/// �Ԃ������G�ƂƂ��ɔ���
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
