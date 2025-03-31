using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの移動を制御するクラス
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
    /// 毎フレームの更新処理
    /// </summary>
    public void Update()
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

        if (status != null)
        {
            status.SetState(CharacterState.Moving);
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        if (rigidBody == null) return;

        // ステータスが取得できない場合や、ステータスが移動中でない場合は移動しない
        if (status == null || status.CurrentState != CharacterState.Moving) return;

        float speed = status.Speed;

        // 移動先に到着しているなら移動を停止
        if (Vector2.Distance(rigidBody.transform.position, destination) <= 0.1f)
        {
            rigidBody.velocity = Vector2.zero;
            status.SetState(CharacterState.Idle);
            hasDestination = false;
            return;
        }

        // 目的地に向かって移動
        Vector2 direction = (destination - rigidBody.transform.position).normalized;
        rigidBody.velocity = direction * speed;
    }
}
