public interface IDamageable
{
    /// <summary>
    /// �_���[�W���󂯂郁�\�b�h
    /// </summary>
    /// <param name="amount">�󂯂�_���[�W�̗�</param>
    /// <param name="attackerTag">�U���҂̃^�O</param>
    void TakeDamage(int amount, string attackerTag);
}
