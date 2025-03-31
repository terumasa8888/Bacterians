using UnityEngine;

public interface IDamageable
{
    void TakeDamageFrom(IDamageable attacker);
    GameObject GameObject { get; }
    IStatus Status { get; } // Status‘S•”Žæ“¾‚Å‚«‚é‚Ì‚Í‚æ‚­‚È‚¢?
}
