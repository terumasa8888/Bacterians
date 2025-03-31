using UnityEngine;
using UniRx;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.Experimental.GraphView;
using System.Collections;

/// <summary>
/// �L�����N�^�[�̊��N���X
/// </summary>
public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    protected IAttackBehaviour attackBehaviour;
    protected IStatus status;
    private RotatorLogic rotator;
    private IDuplicater duplicater;

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

        // Duplicater�̏������ƊJ�n
        duplicater = new Duplicater(status, this);
        StartCoroutine(duplicater.StartDuplicate());

        // �U�����@�̏�����
        attackBehaviour = gameObject.GetComponent<IAttackBehaviour>();
    }

    /// <summary>
    /// �U�����󂯂����Ɏ��s����郁�\�b�h
    /// </summary>
    public void TakeDamageFrom(IDamageable attacker)
    {
        status.TakeDamage(attacker.Status.Attack);
    }

    /// <summary>
    /// �U�����@������������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    protected void InitializeAttackBehaviour<T>() where T : Component, IAttackBehaviour
    {
        attackBehaviour = gameObject.GetComponent<T>();
        if (attackBehaviour == null)
        {
            attackBehaviour = gameObject.AddComponent<T>();
        }
    }

    /*protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag) == false)
        {
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            if (target != null)
            {
                attackBehaviour.Attack(this, target);
            }
        }
    }*/

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

