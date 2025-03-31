using UnityEngine;

/// <summary>
/// �G�L�����N�^�[�̊��N���X
/// </summary>
public abstract class NormalEnemyBase : CharacterBase
{
    // �����炭EnemyBehavior�֌W�̏����������Ă���

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag) == false)
        {
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            if (target != null)
            {
                attackBehaviour.Attack(this, target);
            }
        }
    }
}