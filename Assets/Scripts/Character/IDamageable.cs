using UnityEngine;

public interface IDamageable
{
    void TakeDamageFrom(IDamageable attacker);
    GameObject GameObject { get; }
    IStatus Status { get; } // Status�S���擾�ł���̂͂悭�Ȃ�?
}
