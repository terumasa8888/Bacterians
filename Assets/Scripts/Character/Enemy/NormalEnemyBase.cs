using UnityEngine;

/// <summary>
/// 敵キャラクターの基底クラス
/// </summary>
public abstract class NormalEnemyBase : CharacterBase
{
    // おそらくEnemyBehavior関係の処理を持ってくる

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag) == false)
        {
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            if (target != null)
            {
                attackBehaviour.Attack(this, target);
            }
        }
    }
}