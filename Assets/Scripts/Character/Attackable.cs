using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃を行う機能を提供するクラス
/// </summary>
public class Attackable : MonoBehaviour
{

    private float attackInterval = 2f;
    private float timer;
    private bool isNearby;
    private GameObject currentTarget;
    private int attackPower;

    void Start()
    {
        timer = attackInterval;
        isNearby = false;
        attackPower = GetComponent<Status>().Attack;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (!isNearby || timer > 0f) return;
        if (currentTarget == null) return;

        Attack(currentTarget);
        timer = attackInterval;
    }

    /// <summary>
    /// 接触している間、攻撃対象を設定する
    /// </summary>
    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        isNearby = true;
        currentTarget = collidedObject;
    }

    /// <summary>
    /// 接触が終わったら攻撃対象を解除する
    /// </summary>
    void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        isNearby = false;
        currentTarget = null;
    }

    /// <summary>
    /// 攻撃を行う
    /// </summary>
    void Attack(GameObject target)
    {
        var damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (target.tag == gameObject.tag) return;
            damageable.TakeDamage(attackPower, gameObject.tag);
        }
    }
}
