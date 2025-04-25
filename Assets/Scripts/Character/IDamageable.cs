using UnityEngine;

public interface IDamageable
{
    void TakeDamageFrom(IDamageable attacker);
    GameObject GameObject { get; }
    IStatus Status { get; }
}
