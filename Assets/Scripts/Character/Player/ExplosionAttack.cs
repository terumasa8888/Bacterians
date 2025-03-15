using UnityEngine;

/// <summary>
/// �Ԃ������G�ƂƂ��ɔ�������U��
/// </summary>
public class ExplosionAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] private GameObject explosionEffect; // Unity����A�^�b�`����
    [SerializeField] private float explosionRadius = 1f; // �����̔��a
    [SerializeField] private LayerMask damageableLayer; // �_���[�W��^����Ώۂ̃��C���[
    [SerializeField] private GameObject explosionSoundPrefab; // �������̃v���n�u

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
