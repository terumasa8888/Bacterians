using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーキャラクターの基底クラス
/// </summary>
public abstract class PlayerBase : SmallCharacterBase
{
    private PlayerMovement movement;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        movement = new PlayerMovement(status, rigidBody);
    }

    protected virtual void Update()
    {
        movement.Update();
    }

    /// <summary>
    /// 移動先を設定する
    /// </summary>
    public void SetDestination(Vector3 destination)
    {
        movement.SetDestination(destination);
    }
}
