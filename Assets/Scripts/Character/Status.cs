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
/// </summary>
public class Status : MonoBehaviour, IDamageable
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private ParticleSystem deadEffect;
    [SerializeField] private ParticleSystem healEffect;

    public ReactiveProperty<int> Hp { get; private set; }
    public ReactiveProperty<int> DuplicatableNumber { get; private set; }
    private int attack;
    private float speed;
    private int multiplySpeed;
    private int healPower;
    private float duplicateInterval;

    private Sprite characterSprite;

    public PlayerState CurrentState { get; private set; }

    void Awake()
    {
        Hp = new ReactiveProperty<int>(characterData.Hp);
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
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        Hp.Value += amount;
        Instantiate(healEffect, transform.position, Quaternion.identity);
    }

    public void ReduceDuplicatableNumber()
    {
        if (DuplicatableNumber.Value <= 0) return;
        DuplicatableNumber.Value--;
    }

    public void SetDuplicatableNumber(int number)
    {
        DuplicatableNumber.Value = number;
    }

    public void SetState(PlayerState newState)
    {
        CurrentState = newState;
    }
}
