using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーキャラクターの基底クラス
/// </summary>
public abstract class PlayerBase : CharacterBase
{
    private PlayerMovement movement;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        movement = new PlayerMovement(status, rigidBody);
    }

    protected void Update()
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

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag) == false)
        {
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            if (target != null)
            {
                attackBehaviour.Attack(this, target);
            }
        }
    }

}
