using System;
using UniRx;

public interface IStatus
{
    ReactiveProperty<int> Hp { get; }
    ReactiveProperty<int> DuplicatableNumber { get; }
    float DuplicateInterval { get; }
    PlayerState CurrentState { get; }
    IObservable<Unit> OnDie { get; }

    void TakeDamage(int amount, string attackerTag);
    void Heal(int amount);
    void ReduceDuplicatableNumber();
    void SetDuplicatableNumber(int number);
    void SetState(PlayerState newState);
}
