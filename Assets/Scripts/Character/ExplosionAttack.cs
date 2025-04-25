using UnityEngine;

/// <summary>
/// ぶつかった敵とともに爆発する攻撃
/// </summary>
public class ExplosionAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] private GameObject explosionEffect; // エフェクト
    [SerializeField] private float explosionRadius = 1f; // 爆発の半径
    [SerializeField] private GameObject explosionSound; // 爆発音

    private SoundPlayer soundPlayer;

    public void Start()
    {
        soundPlayer = GetComponent<SoundPlayer>();
    }

    public void Attack(IDamageable attacker, IDamageable target)
    {
        /*Instantiate(explosionEffect, attacker.GameObject.transform.position, Quaternion.identity);

        soundPlayer.PlaySound(explosionSound);*/

        // explosionRadiusの範囲内にいるIDamageableを持っているオブジェクトに攻撃を加える
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(target.GameObject.transform.position, explosionRadius);
        foreach (var hit in hitTargets)
        {
            if (hit.gameObject.CompareTag(attacker.GameObject.tag) == false)
            {
                IDamageable targetObject = hit.GetComponent<IDamageable>();
                if (targetObject != null)
                {
                    targetObject.TakeDamageFrom(attacker);
                }
            }
        }
        // 攻撃者のHPを0に設定して破棄
        attacker.Status.TakeDamage(attacker.Status.Hp);

        // 攻撃エフェクトと音の再生
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, attacker.GameObject.transform.position, Quaternion.identity);
        }
        if (explosionSound != null)
        {
            soundPlayer.PlaySound(explosionSound);
        }
    }
}
