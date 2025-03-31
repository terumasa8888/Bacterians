using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Threading;

/// <summary>
/// 通常攻撃
/// </summary>
public class NormalAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] private GameObject attackEffect; // 攻撃のエフェクト
    [SerializeField] private GameObject attackSound; // 攻撃音
    [SerializeField] private float attackInterval = 2f; // 攻撃のインターバル

    private SoundPlayer soundPlayer;
    private bool canAttack = true;
    private CancellationTokenSource cts;

    public void Start()
    {
        soundPlayer = GetComponent<SoundPlayer>();
        cts = new CancellationTokenSource();
    }

    public void OnDestroy()
    {
        cts.Cancel();
    }

    public void Attack(IDamageable attacker, IDamageable target)
    {
        if (canAttack)
        {
            PerformAttack(attacker, target, cts.Token).Forget();
        }
    }

    private async UniTaskVoid PerformAttack(IDamageable attacker, IDamageable target, CancellationToken token)
    {
        canAttack = false;

        // 攻撃の処理
        target.TakeDamageFrom(attacker);

        // 攻撃エフェクトと音の再生
        if (attackEffect != null)
        {
            Instantiate(attackEffect, attacker.GameObject.transform.position, Quaternion.identity);
        }
        if (attackSound != null)
        {
            soundPlayer.PlaySound(attackSound);
        }

        // インターバルの待機
        await UniTask.Delay(TimeSpan.FromSeconds(attackInterval), cancellationToken: token);

        canAttack = true;
    }
}
