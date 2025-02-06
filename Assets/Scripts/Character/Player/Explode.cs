using UnityEngine;

/// <summary>
/// ‚Ô‚Â‚©‚Á‚½“G‚Æ‚Æ‚à‚É”š”­
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
