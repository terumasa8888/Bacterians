using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�̈ړ��𐧌䂷��N���X
/// </summary>
public class PlayerMovement
{
    private Vector3 destination;
    private bool hasDestination = false;
    private IStatus status;
    private Rigidbody2D rigidBody;

    public PlayerMovement(IStatus status, Rigidbody2D rigidBody)
    {
        this.status = status;
        this.rigidBody = rigidBody;
    }

    /// <summary>
    /// ���t���[���̍X�V����
    /// </summary>
    public void Update()
    {
        if (hasDestination)
        {
            Move();
        }
    }

    /// <summary>
    /// �ړ����ݒ肷��
    /// </summary>
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        hasDestination = true;

        if (status != null)
        {
            status.SetState(CharacterState.Moving);
        }
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void Move()
    {
        if (rigidBody == null) return;

        // �X�e�[�^�X���擾�ł��Ȃ��ꍇ��A�X�e�[�^�X���ړ����łȂ��ꍇ�͈ړ����Ȃ�
        if (status == null || status.CurrentState != CharacterState.Moving) return;

        float speed = status.Speed;

        // �ړ���ɓ������Ă���Ȃ�ړ����~
        if (Vector2.Distance(rigidBody.transform.position, destination) <= 0.1f)
        {
            rigidBody.velocity = Vector2.zero;
            status.SetState(CharacterState.Idle);
            hasDestination = false;
            return;
        }

        // �ړI�n�Ɍ������Ĉړ�
        Vector2 direction = (destination - rigidBody.transform.position).normalized;
        rigidBody.velocity = direction * speed;
    }
}
