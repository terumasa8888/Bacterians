/*using System;
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

    private int hp;
    public int Hp
    {
        get => hp;
        private set
        {
            hp = value;
            if (hp <= 0)
            {
                Die();
            }
        }
    }
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
        Hp = characterData.Hp;
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
        get => attack;
        private set => attack = value;
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
        Attack *= multiplier;
    }

    public virtual void TakeDamage(int amount, string attackerTag)
    {
        Hp -= amount;
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
        Hp += amount;
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
*/

using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// キャラクターのステータスを管理するクラス
/// MonoBehaviour を継承せず、データ管理に専念
/// </summary>
public class Status : MonoBehaviour, IStatus
{
    public int Hp { get; private set; }
    public int Attack { get; private set; }
    public float Speed { get; private set; }
    public int MultiplySpeed { get; private set; }
    public int HealPower { get; private set; }
    public float DuplicateInterval { get; private set; }

    public ReactiveProperty<int> DuplicatableNumber { get; private set; }
    public PlayerState CurrentState { get; private set; }

    public IObservable<Unit> OnDie => onDie;
    private Subject<Unit> onDie = new Subject<Unit>();

    /// <summary>
    /// ステータスの初期化
    /// </summary>
    public Status(CharacterData characterData)
    {
        Hp = characterData.Hp;
        Attack = characterData.Attack;
        Speed = characterData.Speed;
        MultiplySpeed = characterData.MultiplySpeed;
        HealPower = characterData.HealPower;
        DuplicatableNumber = new ReactiveProperty<int>(characterData.DuplicatableNumber);
        DuplicateInterval = characterData.DuplicateInterval;
    }


    public void MultiplyAttack(int multiplier)
    {
        Attack *= multiplier;
    }
    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void TakeDamage(int amount, string cachedTag)
    {
        Hp -= amount;
        if (Hp <= 0)
        {
            onDie.OnNext(Unit.Default);
            onDie.OnCompleted();
        }
    }

    /// <summary>
    /// HPを回復
    /// </summary>
    public void Heal(int amount)
    {
        Hp += amount;
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
public enum PlayerState
{
    Idle,
    Selected,
    Moving
}
