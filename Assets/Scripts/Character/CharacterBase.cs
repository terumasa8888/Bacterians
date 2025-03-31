using UnityEngine;
using UniRx;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.Experimental.GraphView;
using System.Collections;

/// <summary>
/// キャラクターの基底クラス
/// </summary>
public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    protected IAttackBehaviour attackBehaviour;
    protected IStatus status;
    private RotatorLogic rotator;
    private IDuplicater duplicater;

    [SerializeField] private CharacterData characterData;
    [SerializeField] private GameObject deadEffect;

    public GameObject GameObject => gameObject;
    public IStatus Status => status;
    public RotatorLogic Rotator => rotator; // RotatorLogicのインスタンスを公開

    protected virtual void Awake()
    {
        // ステータスの初期化
        status = new Status(characterData);
        GetComponent<SpriteRenderer>().sprite = status.CharacterSprite;

        // Rotatorの初期化
        rotator = new RotatorLogic(transform);
        StartCoroutine(rotator.UpdateRotation());

        // 死亡時の処理
        status.OnDie.Subscribe(_ => OnDie());

        // Duplicaterの初期化と開始
        duplicater = new Duplicater(status, this);
        StartCoroutine(duplicater.StartDuplicate());

        // 攻撃方法の初期化
        attackBehaviour = gameObject.GetComponent<IAttackBehaviour>();
    }

    /// <summary>
    /// 攻撃を受けた時に実行されるメソッド
    /// </summary>
    public void TakeDamageFrom(IDamageable attacker)
    {
        status.TakeDamage(attacker.Status.Attack);
    }

    /// <summary>
    /// 攻撃方法を初期化する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    protected void InitializeAttackBehaviour<T>() where T : Component, IAttackBehaviour
    {
        attackBehaviour = gameObject.GetComponent<T>();
        if (attackBehaviour == null)
        {
            attackBehaviour = gameObject.AddComponent<T>();
        }
    }

    /*protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag(collision.gameObject.tag) == false)
        {
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            if (target != null)
            {
                attackBehaviour.Attack(this, target);
            }
        }
    }*/

    /// <summary>
    /// 攻撃方法を入れ替える時に実行されるメソッド
    /// </summary>
    public void SetAttackBehaviour(IAttackBehaviour newAttackBehaviour)
    {
        // 現在アタッチされているIAttackBehaviour型のコンポーネントを削除
        if (attackBehaviour != null)
        {
            Destroy(GetComponent<IAttackBehaviour>() as Component);
        }
        // 新しい攻撃方法をアタッチ
        attackBehaviour = newAttackBehaviour;
        gameObject.AddComponent(newAttackBehaviour.GetType());
    }

    private void OnDie()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

