using UnityEngine;

/// <summary>
/// キャラクターの基底クラス
/// </summary>
public abstract class CharacterBase : MonoBehaviour
{
    protected IAttackBehaviour attackBehaviour;
    private Status status;

    /// <summary>
    /// 開始時に呼び出されるメソッド
    /// </summary>
    protected void Awake()
    {
        status = GetComponent<Status>();
        if (status != null)
        {
            InitializeAttackBehaviour(status);
        }
        else
        {
            Debug.LogError("Status component not found on " + gameObject.name);
        }
    }

    /// <summary>
    /// 攻撃パターンを初期化するメソッド
    /// </summary>
    protected abstract void InitializeAttackBehaviour(Status status);

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var targetStatus = collision.gameObject.GetComponent<Status>();
            if (targetStatus != null)
            {
                attackBehaviour.Attack(targetStatus);
            }
        }
    }

    /// <summary>
    /// 攻撃パターンを設定するメソッド
    /// </summary>
    public void SetAttackBehaviour(IAttackBehaviour attackBehaviour)
    {
        this.attackBehaviour = attackBehaviour;
    }
}
