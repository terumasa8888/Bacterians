using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// Enemyに接触した際に爆発を起こす機能を提供するクラス
/// </summary>
public class Explodable : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect; // 爆発エフェクトのプレハブ
    [SerializeField] private float explosionRadius = 1f; // 爆発の半径
    [SerializeField] private LayerMask damageableLayer; // ダメージを与える対象のレイヤー
    [SerializeField] private GameObject explosionSoundPrefab; // 爆発音のプレハブ

    private int attackPower;
    private string cachedTag;

    private SoundPlayer soundPlayer;

    void Start()
    {
        attackPower = GetComponent<Status>().Attack;
        cachedTag = gameObject.tag;
        soundPlayer = GetComponent<SoundPlayer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    /// <summary>
    /// 爆発攻撃
    /// </summary>
    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        soundPlayer.PlaySound(explosionSoundPrefab);

        // 範囲攻撃
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayer);
        foreach (var hitCollider in hitColliders)
        {
            var damageable = hitCollider.GetComponent<IStatus>();
            if (damageable != null && (hitCollider.CompareTag("Item") || hitCollider.CompareTag("Enemy")))
            {
                damageable.TakeDamage(attackPower, cachedTag);
            }
        }
        Destroy(gameObject);
    }
}
