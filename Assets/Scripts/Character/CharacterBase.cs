using UnityEngine;
using UniRx;
using System.Collections;

/// <summary>
/// キャラクターの基底クラス
/// </summary>
public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    protected IAttackBehaviour attackBehaviour;
    protected IStatus status;
    private RotatorLogic rotator;

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
    }

    /// <summary>
    /// 攻撃手法を初期化する
    /// </summary>
    public void InitializeAttackBehaviour()
    {
        attackBehaviour = gameObject.GetComponent<IAttackBehaviour>();
        if (attackBehaviour == null)
        {
            Debug.LogError($"{gameObject.name} に IAttackBehaviour がアタッチされていません。");
        }
    }

    /// <summary>
    /// 攻撃を受けた時に実行されるメソッド
    /// </summary>
    public void TakeDamageFrom(IDamageable attacker)
    {
        status.TakeDamage(attacker.Status.Attack);
    }

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

