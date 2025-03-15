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
    /// フェーズとターゲットを設定する
    /// </summary>
    public void SetState(EnemyState state, Transform target)
    {
        currentState = state;
        currentTarget = target;
        PaintForDebug(state);
    }

    /// <summary>
    /// フェーズに応じて色を変更する
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
    /// 現在のターゲットを追跡する
    /// </summary>
    private void Trace()
    {
        if (currentTarget == null) return;

        nav.SetDestination(currentTarget.position);
    }
}
