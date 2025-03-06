using UnityEngine;
using UniRx;

/// <summary>
/// �L�����N�^�[�̊��N���X
/// </summary>
public abstract class CharacterBase : DamageableBase
{
    protected IAttackBehaviour attackBehaviour;

    /// <summary>
    /// �U���p�^�[�������������郁�\�b�h
    /// </summary>
    protected void InitializeAttackBehaviour<T>() where T : Component, IAttackBehaviour
    {
        IAttackBehaviour attackBehaviour = gameObject.GetComponent<T>();
        if (attackBehaviour == null)
        {
            attackBehaviour = gameObject.AddComponent<T>();
        }
        SetAttackBehaviour(attackBehaviour);
    }

    // ���ۃ��\�b�h�ł����@�����邪�A�����蔻��ɂ͍���Ȃ������B�R���|�W�V�����̂ق��������Ă�Ǝv��
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageableBase target = collision.gameObject.GetComponent<DamageableBase>();
            if (target != null)
            {
                attackBehaviour.Attack(gameObject, target);
            }
        }
    }

    /// <summary>
    /// �U���p�^�[����ݒ肷�郁�\�b�h
    /// </summary>
    public void SetAttackBehaviour(IAttackBehaviour attackBehaviour)
    {
        this.attackBehaviour = attackBehaviour;
    }
}

