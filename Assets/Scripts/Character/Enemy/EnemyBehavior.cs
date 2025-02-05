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
    /// フェーズとターゲットを設定する
    /// </summary>
    public void SetPhase(EnemyPhase phase, Transform target)
    {
        currentPhase = phase;
        currentTarget = target;
        PaintForDebug(phase);
    }

    /// <summary>
    /// フェーズに応じて色を変更する
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
    /// 現在のターゲットを追跡する
    /// </summary>
    private void Trace()
    {
        if (currentTarget == null) return;

        nav.SetDestination(currentTarget.position);
    }
}
