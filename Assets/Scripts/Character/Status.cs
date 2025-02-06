using System;
using System.Collections;
using UnityEngine;
using UniRx;

public enum PlayerState
{
    Idle,
    Selected,
    Moving
}

/// <summary>
/// キャラクターのステータスを管理するクラス
/// MonoBehaviourをなくせる？
/// </summary>
public class Status : MonoBehaviour, IStatus
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private ParticleSystem deadEffect;

    public ReactiveProperty<int> Hp { get; private set; }
    public ReactiveProperty<int> DuplicatableNumber { get; private set; }
    private int attack;
    private float speed;
    private int multiplySpeed;
    private int healPower;
    private float duplicateInterval;

    private Sprite characterSprite;

    public PlayerState CurrentState { get; private set; }

    // キャラクターが死んだことを通知するイベント
    public IObservable<Unit> OnDie => onDie;
    private Subject<Unit> onDie = new Subject<Unit>();

    //ステータスの初期化
    void Awake()
    {
        Hp = new ReactiveProperty<int>(characterData.Hp);//ReactivePropertyを使う必要性？
        attack = characterData.Attack;
        speed = characterData.Speed;
        multiplySpeed = characterData.MultiplySpeed;
        healPower = characterData.HealPower;
        DuplicatableNumber = new ReactiveProperty<int>(characterData.DuplicatableNumber);
        duplicateInterval = characterData.DuplicateInterval;

        characterSprite = characterData.CharacterSprite;
        GetComponent<SpriteRenderer>().sprite = characterSprite;

        CurrentState = PlayerState.Idle;
    }

    public int Attack
    {
        get { return attack; }
    }

    public float Speed
    {
        get { return speed; }
    }

    public int HealPower
    {
        get { return healPower; }
    }

    public float DuplicateInterval
    {
        get { return duplicateInterval; }
    }

    public void MultiplyAttack(int multiplier)
    {
        attack *= multiplier;
    }

    public virtual void TakeDamage(int amount, string attackerTag)
    {
        Hp.Value -= amount;
        if (Hp.Value <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        onDie.OnNext(Unit.Default); // 死亡イベントを発行
        onDie.OnCompleted(); // イベントの完了を通知
        Destroy(gameObject);
    }

    /// <summary>
    /// 回復処理
    /// </summary>
    public void Heal(int amount)
    {
        Hp.Value += amount;
    }

    /// <summary>
    /// 増殖可能回数を減らす
    /// </summary>
    public void ReduceDuplicatableNumber()
    {
        if (DuplicatableNumber.Value <= 0) return;
        DuplicatableNumber.Value--;
    }

    /// <summary>
    /// 増殖可能回数を設定する
    /// </summary>
    public void SetDuplicatableNumber(int number)
    {
        DuplicatableNumber.Value = number;
    }

    /// <summary>
    /// キャラクターの現在の状態を設定する
    /// </summary>
    public void SetState(PlayerState newState)
    {
        CurrentState = newState;
    }
}
