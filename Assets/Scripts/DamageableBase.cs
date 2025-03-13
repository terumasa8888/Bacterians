using UnityEngine;
using UniRx;

/// <summary>
/// ダメージを受けるオブジェクトの基底クラス
/// </summary>
public abstract class DamageableBase : MonoBehaviour
{
    private IStatus status;

    [SerializeField] private CharacterData characterData;
    [SerializeField] private ParticleSystem deadEffect;
    public GameObject GameObject => gameObject;
    public IStatus Status => status;

    /// <summary>
    /// 開始時に呼び出されるメソッド
    /// </summary>
    protected virtual void Awake()
    {
        status = new Status(characterData);
        GetComponent<SpriteRenderer>().sprite = characterData.CharacterSprite;

        status.OnDie.Subscribe(_ => Die());
    }

    /// <summary>
    /// ダメージを受けるメソッド
    /// </summary>
    public void TakeDamage(GameObject attacker)
    {
        DamageableBase attackerDamageable = attacker.GetComponent<DamageableBase>();
        if (attackerDamageable != null)
        {
            status.TakeDamage(attackerDamageable.Status.Attack, null);
        }
    }

    /// <summary>
    /// 死亡時の処理
    /// </summary>
    protected virtual void Die()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
