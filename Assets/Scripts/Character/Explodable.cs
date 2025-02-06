using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// Enemy�ɐڐG�����ۂɔ������N�����@�\��񋟂���N���X
/// </summary>
public class Explodable : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect; // �����G�t�F�N�g�̃v���n�u
    [SerializeField] private float explosionRadius = 1f; // �����̔��a
    [SerializeField] private LayerMask damageableLayer; // �_���[�W��^����Ώۂ̃��C���[
    [SerializeField] private GameObject explosionSoundPrefab; // �������̃v���n�u

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
    /// �����U��
    /// </summary>
    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        soundPlayer.PlaySound(explosionSoundPrefab);

        // �͈͍U��
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
