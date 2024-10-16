using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UniRx;


/// <summary>
/// ボス敵の弾(ザコ敵)のスクリプト
/// 画面外で自動で消滅する
/// </summary>
public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    private Rigidbody2D rid2d;
    private ObjectPool<GameObject> pool;

    public void Initialize(float velocity, float radian)
    {
        rid2d = GetComponent<Rigidbody2D>();

        Vector2 bulletV = new Vector2(
            velocity * Mathf.Cos(radian),
            velocity * Mathf.Sin(radian)
        );
        rid2d.velocity = bulletV;

        // 回転処理
        rid2d.angularVelocity = 360f;

    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            if (pool != null)
            {
                pool.Release(gameObject);
            }
        }
    }

    void OnBecameInvisible()
    {
        if (pool != null)
        {
            pool.Release(gameObject);
        }
    }
}
