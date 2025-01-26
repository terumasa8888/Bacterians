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

    private void Start()
    {
        nav = GetComponent<NavMeshAgent2D>();
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
