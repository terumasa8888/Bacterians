using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの移動を制御するスクリプト
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Vector3 destination;
    private bool hasDestination = false;

    private void Update()
    {
        if (hasDestination)
        {
            Move();
        }
    }

    /// <summary>
    /// 移動先を設定する
    /// </summary>
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        hasDestination = true;

        Status status = GetComponent<Status>();
        if (status != null)
        {
            status.SetState(PlayerState.Moving);
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        Status status = GetComponent<Status>();
        if (status == null || status.CurrentState != PlayerState.Moving) return;

        float speed = status.Speed;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // 移動先に到着したら移動を停止
        if (Vector2.Distance(transform.position, destination) <= 0.1f)
        {
            rb.velocity = Vector2.zero;
            status.SetState(PlayerState.Idle);
            hasDestination = false;
            return;
        }

        Vector2 direction = (destination - transform.position).normalized;
        rb.velocity = direction * speed;
    }
}
