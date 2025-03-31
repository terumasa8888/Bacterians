using UnityEngine;

/// <summary>
/// �Ԃ������G�ƂƂ��ɔ�������U��
/// </summary>
public class ExplosionAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] private GameObject explosionEffect; // �G�t�F�N�g
    [SerializeField] private float explosionRadius = 1f; // �����̔��a
    [SerializeField] private GameObject explosionSound; // ������

    private SoundPlayer soundPlayer;

    public void Start()
    {
        soundPlayer = GetComponent<SoundPlayer>();
    }

    public void Attack(IDamageable attacker, IDamageable target)
    {
        /*Instantiate(explosionEffect, attacker.GameObject.transform.position, Quaternion.identity);

        soundPlayer.PlaySound(explosionSound);*/

        // explosionRadius�͈͓̔��ɂ���IDamageable�������Ă���I�u�W�F�N�g�ɍU����������
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
        // �U���҂�HP��0�ɐݒ肵�Ĕj��
        attacker.Status.TakeDamage(attacker.Status.Hp);

        // �U���G�t�F�N�g�Ɖ��̍Đ�
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
