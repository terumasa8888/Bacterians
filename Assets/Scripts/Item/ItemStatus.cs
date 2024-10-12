using UnityEngine;
using UniRx; 

public class ItemStatus : Status
{
    private string lastAttackerTag;

    // IDamageable �C���^�[�t�F�[�X�̃��\�b�h���I�[�o�[���C�h
    public override void TakeDamage(int amount, string attackerTag)
    {
        lastAttackerTag = attackerTag;
        base.TakeDamage(amount, attackerTag);
    }

    protected override void Die()
    {
        MessageBroker.Default.Publish(new ItemDestroyedMessage(lastAttackerTag));
        base.Die();
    }
}
