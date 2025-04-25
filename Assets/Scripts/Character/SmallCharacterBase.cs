using UnityEngine;
using System.Collections;
using System.Reflection.Emit;

/// <summary>
/// ���^�L�����N�^�[�̊��N���X
/// </summary>
public abstract class SmallCharacterBase : CharacterBase
{
    private IDuplicater duplicater;

    protected override void Awake()
    {
        base.Awake();

        // Duplicater�̏������ƊJ�n
        duplicater = new Duplicater(status, this);
        StartCoroutine(duplicater.StartDuplicate());
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag) == false)
        {
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            if (target != null)
            {
                if (attackBehaviour != null) // null�`�F�b�N��ǉ�
                {
                    attackBehaviour.Attack(this, target);
                }
                else
                {
                    Debug.LogWarning("attackBehaviour is not initialized.", this);
                }
            }
        }
    }
}
