using UnityEngine;
using UniRx;
using System.Collections;

/// <summary>
/// �L�����N�^�[�̊��N���X
/// </summary>
public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    protected IAttackBehaviour attackBehaviour;
    protected IStatus status;
    private RotatorLogic rotator;

    [SerializeField] private CharacterData characterData;
    [SerializeField] private GameObject deadEffect;

    public GameObject GameObject => gameObject;
    public IStatus Status => status;
    public RotatorLogic Rotator => rotator; // RotatorLogic�̃C���X�^���X�����J

    protected virtual void Awake()
    {
        // �X�e�[�^�X�̏�����
        status = new Status(characterData);
        GetComponent<SpriteRenderer>().sprite = status.CharacterSprite;

        // Rotator�̏�����
        rotator = new RotatorLogic(transform);
        StartCoroutine(rotator.UpdateRotation());

        // ���S���̏���
        status.OnDie.Subscribe(_ => OnDie());
    }

    /// <summary>
    /// �U����@������������
    /// </summary>
    public void InitializeAttackBehaviour()
    {
        attackBehaviour = gameObject.GetComponent<IAttackBehaviour>();
        if (attackBehaviour == null)
        {
            Debug.LogError($"{gameObject.name} �� IAttackBehaviour ���A�^�b�`����Ă��܂���B");
        }
    }

    /// <summary>
    /// �U�����󂯂����Ɏ��s����郁�\�b�h
    /// </summary>
    public void TakeDamageFrom(IDamageable attacker)
    {
        status.TakeDamage(attacker.Status.Attack);
    }

    /// <summary>
    /// �U�����@�����ւ��鎞�Ɏ��s����郁�\�b�h
    /// </summary>
    public void SetAttackBehaviour(IAttackBehaviour newAttackBehaviour)
    {
        // ���݃A�^�b�`����Ă���IAttackBehaviour�^�̃R���|�[�l���g���폜
        if (attackBehaviour != null)
        {
            Destroy(GetComponent<IAttackBehaviour>() as Component);
        }
        // �V�����U�����@���A�^�b�`
        attackBehaviour = newAttackBehaviour;
        gameObject.AddComponent(newAttackBehaviour.GetType());
    }

    private void OnDie()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

