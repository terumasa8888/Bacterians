using UnityEngine;
public interface IDamageable
{
    void TakeDamage(GameObject attacker);
    GameObject GameObject { get; }
    IStatus Status { get; }
}
