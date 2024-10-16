using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UniRx;


/// <summary>
/// �{�X�G�̒e(�U�R�G)�̃X�N���v�g
/// ��ʊO�Ŏ����ŏ��ł���
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

        // ��]����
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
