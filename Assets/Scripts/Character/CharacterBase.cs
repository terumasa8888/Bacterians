using UnityEngine;
using UniRx;

/// <summary>
/// キャラクターの基底クラス
/// </summary>
public abstract class CharacterBase : DamageableBase
{
    protected IAttackBehaviour attackBehaviour;

    /// <summary>
    /// 攻撃パターンを初期化するメソッド
    /// </summary>
    protected void InitializeAttackBehaviour<T>() where T : Component, IAttackBehaviour
    {
        IAttackBehaviour attackBehaviour = gameObject.GetComponent<T>();
        if (attackBehaviour == null)
        {
            attackBehaviour = gameObject.AddComponent<T>();
        }
        SetAttackBehaviour(attackBehaviour);
    }

    // 抽象メソッドでやる方法もあるが、当たり判定には合わないかも。コンポジションのほうがあってると思う
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageableBase target = collision.gameObject.GetComponent<DamageableBase>();
            if (target != null)
            {
                attackBehaviour.Attack(gameObject, target);
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

