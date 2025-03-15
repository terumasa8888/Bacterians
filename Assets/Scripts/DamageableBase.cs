using UnityEngine;
using UniRx;

/// <summary>
/// �_���[�W���󂯂�I�u�W�F�N�g�̊��N���X
/// </summary>
public abstract class DamageableBase : MonoBehaviour
{
    private IStatus status;

    [SerializeField] private CharacterData characterData;
    [SerializeField] private ParticleSystem deadEffect;
    public GameObject GameObject => gameObject;
    public IStatus Status => status;

    /// <summary>
    /// �J�n���ɌĂяo����郁�\�b�h
    /// </summary>
    protected virtual void Awake()
    {
        status = new Status(characterData);
        GetComponent<SpriteRenderer>().sprite = characterData.CharacterSprite;

        status.OnDie.Subscribe(_ => Die());
    }

    /// <summary>
    /// �_���[�W���󂯂郁�\�b�h
    /// </summary>
    public void TakeDamage(GameObject attacker)
    {
        DamageableBase attackerDamageable = attacker.GetComponent<DamageableBase>();
        if (attackerDamageable != null)
        {
            status.TakeDamage(attackerDamageable.Status.Attack, null);
        }
    }

    /// <summary>
    /// ���S���̏���
    /// </summary>
    protected virtual void Die()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
