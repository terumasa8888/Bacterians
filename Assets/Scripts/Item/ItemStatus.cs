using UnityEngine;
using UniRx; 

public class ItemStatus : Status
{
    private string lastAttackerTag;

    // IDamageable インターフェースのメソッドをオーバーライド
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
