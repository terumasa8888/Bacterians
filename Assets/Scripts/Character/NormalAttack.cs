using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Threading;

/// <summary>
/// �ʏ�U��
/// </summary>
public class NormalAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] private GameObject attackEffect; // �U���̃G�t�F�N�g
    [SerializeField] private GameObject attackSound; // �U����
    [SerializeField] private float attackInterval = 2f; // �U���̃C���^�[�o��

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

        // �U���̏���
        target.TakeDamageFrom(attacker);

        // �U���G�t�F�N�g�Ɖ��̍Đ�
        if (attackEffect != null)
        {
            Instantiate(attackEffect, attacker.GameObject.transform.position, Quaternion.identity);
        }
        if (attackSound != null)
        {
            soundPlayer.PlaySound(attackSound);
        }

        // �C���^�[�o���̑ҋ@
        await UniTask.Delay(TimeSpan.FromSeconds(attackInterval), cancellationToken: token);

        canAttack = true;
    }
}
