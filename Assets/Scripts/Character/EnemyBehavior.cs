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
    private EnemyPhase currentPhase;
    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (currentPhase == EnemyPhase.Attack || currentPhase == EnemyPhase.CollectItem)
        {
            Trace();
        }
    }

    /// <summary>
    /// �t�F�[�Y�ƃ^�[�Q�b�g��ݒ肷��
    /// </summary>
    public void SetPhase(EnemyPhase phase, Transform target)
    {
        currentPhase = phase;
        currentTarget = target;
        PaintForDebug(phase);
    }

    /// <summary>
    /// �t�F�[�Y�ɉ����ĐF��ύX����
    /// </summary>
    private void PaintForDebug(EnemyPhase phase)
    {
        switch (phase)
        {
            case EnemyPhase.Attack:
                spriteRenderer.color = Color.red;
                break;
            case EnemyPhase.CollectItem:
                spriteRenderer.color = Color.green;
                break;
            case EnemyPhase.Wait:
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
