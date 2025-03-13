using System;
using UniRx;

public interface IStatus
{
    int Hp { get; }
    int Attack { get; }
    float Speed { get; }
    int MultiplySpeed { get; }
    int HealPower { get; }
    float DuplicateInterval { get; }
    ReactiveProperty<int> DuplicatableNumber { get; }
    PlayerState CurrentState { get; }
    IObservable<Unit> OnDie { get; }

    void TakeDamage(int amount, string cachedTag);
    void Heal(int amount);
    void ReduceDuplicatableNumber();
    void SetDuplicatableNumber(int number);
    void SetState(PlayerState newState);
}
