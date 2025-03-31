using System;
using System.Collections;
using UnityEngine;
using UniRx;

// 敵も実装するわけだから、CharacterStateはStatusクラスに持たせるべきではない？
/*public enum CharacterState
{
    Idle,
    Selected,
    Moving
}*/
public enum CharacterState
{
    Idle,
    Selected,
    Moving,
    Attack,
    CollectItem
}


/// <summary>
/// キャラクターのステータスを管理するクラス
/// </summary>
public class Status : IStatus
{
    public int Hp { get; private set; }
    public int Attack { get; private set; }
    public float Speed { get; private set; }
    public int HealPower { get; private set; }
    public float DuplicateInterval { get; private set; }
    public Sprite CharacterSprite { get; private set; }
    public CharacterState CurrentState { get; private set; }
    public ReactiveProperty<int> DuplicatableNumber { get; private set; }

    private Subject<Unit> onDie = new Subject<Unit>();
    public IObservable<Unit> OnDie => onDie;

    // ステータスの初期化
    public Status(CharacterData characterData)
    {
        Hp = characterData.Hp;
        Attack = characterData.Attack;
        Speed = characterData.Speed;
        HealPower = characterData.HealPower;
        DuplicatableNumber = new ReactiveProperty<int>(characterData.DuplicatableNumber);
        DuplicateInterval = characterData.DuplicateInterval;
        CharacterSprite = characterData.CharacterSprite;
        CurrentState = CharacterState.Idle;
    }

    public void MultiplyAttack(int multiplier)
    {
        Attack *= multiplier;
    }

    public virtual void TakeDamage(int amount)
    {
        Hp -= amount;
        if (Hp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        onDie.OnNext(Unit.Default); // 死亡イベントを発行
        onDie.OnCompleted(); // イベントの完了を通知
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
    public void SetState(CharacterState newState)
    {
        CurrentState = newState;
    }
}
