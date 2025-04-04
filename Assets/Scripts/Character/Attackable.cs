﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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
    private string cachedTag;

    private SoundPlayer soundPlayer;
    [SerializeField] private GameObject attackSoundPrefab;

    void Start()
    {
        timer = attackInterval;
        isNearby = false;
        attackPower = GetComponent<Status>().Attack;
        cachedTag = gameObject.tag;
        soundPlayer = GetComponent<SoundPlayer>();
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
        var damageable = target.GetComponent<Status>();
        if (damageable != null)
        {
            if (cachedTag == target.tag) return;
            //damageable.TakeDamage(attackPower, cachedTag);
            soundPlayer.PlaySound(attackSoundPrefab);
        }
    }
}
