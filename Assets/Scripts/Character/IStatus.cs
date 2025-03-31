using System;
using UniRx;
using UnityEngine;

public interface IStatus
{
    int Hp { get; }
    int Attack { get; }
    float Speed { get; }

    int HealPower { get; }
    float DuplicateInterval { get; }
    Sprite CharacterSprite { get; }
    ReactiveProperty<int> DuplicatableNumber { get; }
    CharacterState CurrentState { get; }
    IObservable<Unit> OnDie { get; }

    void TakeDamage(int amount);
    void Heal(int amount);
    void ReduceDuplicatableNumber();
    void SetDuplicatableNumber(int number);
    void SetState(CharacterState newState);
}
