using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̍s���𐧌䂷��X�N���v�g
/// </summary>
public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent2D nav;
    private EnemyState currentState;
    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (currentState == EnemyState.Attack || currentState == EnemyState.CollectItem)
        {
            Trace();
        }
    }

    /// <summary>
    /// �t�F�[�Y�ƃ^�[�Q�b�g��ݒ肷��
    /// </summary>
    public void SetState(EnemyState state, Transform target)
    {
        currentState = state;
        currentTarget = target;
        PaintForDebug(state);
    }

    /// <summary>
    /// �t�F�[�Y�ɉ����ĐF��ύX����
    /// </summary>
    private void PaintForDebug(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Attack:
                spriteRenderer.color = Color.red;
                break;
            case EnemyState.CollectItem:
                spriteRenderer.color = Color.green;
                break;
            case EnemyState.Idle:
                spriteRenderer.color = Color.blue;
                break;
        }
    }

    /// <summary>
    /// ���݂̃^�[�Q�b�g��ǐՂ���
    /// </summary>
    private void Trace()
    {
        if (currentTarget == null) return;

        nav.SetDestination(currentTarget.position);
    }
}
