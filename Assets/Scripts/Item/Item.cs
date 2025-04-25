using UnityEngine;
using UniRx;

/// <summary>
/// アイテムクラス
/// </summary>
public class Item : CharacterBase
{
    [SerializeField] private int attackMultiplier = 3; // 攻撃力の倍率
    private string lastAttackerTag; // とどめを刺したキャラクターのタグ

    protected override void Awake()
    {
        base.Awake();

        // OnDieイベントを購読してアイテム破壊処理を実行
        status.OnDie.Subscribe(_ => HandleOnDie()).AddTo(this);
    }

    /// <summary>
    /// 衝突時に呼び出され、攻撃してきたキャラクターのタグを記録
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastAttackerTag = collision.gameObject.tag;
    }

    /// <summary>
    /// 死亡時に呼び出され、とどめを刺したキャラクターのタグを持つキャラクターの攻撃力を増加
    /// </summary>
    private void HandleOnDie()
    {
        if (!string.IsNullOrEmpty(lastAttackerTag))
        {
            MultiplyAttack(lastAttackerTag);
            Debug.Log($"Attacker Tag: {lastAttackerTag}");
        }
    }

    /// <summary>
    /// 指定されたタグを持つキャラクターの攻撃力を増加
    /// </summary>
    private void MultiplyAttack(string attackerTag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(attackerTag);
        foreach (GameObject target in targets)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null && damageable.Status != null)
            {
                damageable.Status.MultiplyAttack(attackMultiplier);
            }
        }
    }
}
