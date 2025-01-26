using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の行動を制御するスクリプト
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
    /// フェーズとターゲットを設定する
    /// </summary>
    public void SetPhase(EnemyPhase phase, Transform target)
    {
        currentPhase = phase;
        currentTarget = target;
    }

    /// <summary>
    /// 現在のターゲットを追跡する
    /// </summary>
    private void Trace()
    {
        if (currentTarget == null) return;

        nav.SetDestination(currentTarget.position);
    }
}
