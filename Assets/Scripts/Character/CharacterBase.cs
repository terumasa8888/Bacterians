using UnityEngine;

/// <summary>
/// �L�����N�^�[�̊��N���X
/// </summary>
public abstract class CharacterBase : MonoBehaviour
{
    protected IAttackBehaviour attackBehaviour;
    private Status status;

    /// <summary>
    /// �J�n���ɌĂяo����郁�\�b�h
    /// </summary>
    protected void Awake()
    {
        status = GetComponent<Status>();
        if (status != null)
        {
            InitializeAttackBehaviour(status);
        }
        else
        {
            Debug.LogError("Status component not found on " + gameObject.name);
        }
    }

    /// <summary>
    /// �U���p�^�[�������������郁�\�b�h
    /// </summary>
    protected abstract void InitializeAttackBehaviour(Status status);

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var targetStatus = collision.gameObject.GetComponent<Status>();
            if (targetStatus != null)
            {
                attackBehaviour.Attack(targetStatus);
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
