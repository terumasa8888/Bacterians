using UnityEngine;

/// <summary>
/// ぶつかった敵とともに爆発する攻撃
/// </summary>
public class ExplosionAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] private GameObject explosionEffect; // Unityからアタッチする
    [SerializeField] private float explosionRadius = 1f; // 爆発の半径
    [SerializeField] private LayerMask damageableLayer; // ダメージを与える対象のレイヤー
    [SerializeField] private GameObject explosionSoundPrefab; // 爆発音のプレハブ

    private SoundPlayer soundPlayer;

    public void Initialize()
    {
        soundPlayer = GetComponent<SoundPlayer>();
    }

    public void Attack(GameObject attacker, DamageableBase target)
    {
        Instantiate(explosionEffect, attacker.transform.position, Quaternion.identity);

        soundPlayer.PlaySound(explosionSoundPrefab);

        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attacker.transform.position, explosionRadius, damageableLayer);
        foreach (var hit in hitTargets)
        {
            DamageableBase targetDamageable = hit.GetComponent<DamageableBase>();
            if (targetDamageable != null)
            {
                targetDamageable.TakeDamage(attacker);
            }
        }
        Destroy(gameObject);
    }
}
