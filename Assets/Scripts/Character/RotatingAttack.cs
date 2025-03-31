using System.Collections;
using UnityEngine;
using UniRx;

/// <summary>
/// 回転しながら、触れた敵にダメージを与える攻撃
/// </summary>
public class RotatingAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private float minAttackDuration = 3f; // 攻撃する時間の最小値
    [SerializeField] private float maxAttackDuration = 5f; // 攻撃する時間の最大値
    [SerializeField] private float minWaitTime =5f; // 攻撃のインターバルの最小値
    [SerializeField] private float maxWaitTime = 7f; // 攻撃のインターバルの最大値
    [SerializeField] private float detectionRadius = 3f; // 攻撃対象を検出する半径

    [SerializeField] private GameObject attackEffect; // 攻撃のエフェクト
    [SerializeField] private GameObject attackSound; // 攻撃音

    private bool isAttacking = false;
    private float attackDuration;
    private float waitTime;

    private CharacterBase character;

    private void Start()
    {
        // ランダムな回転時間とインターバルを決定
        attackDuration = Random.Range(minAttackDuration, maxAttackDuration);
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        // CharacterBaseのインスタンスを取得
        character = GetComponent<CharacterBase>();

        // 一定時間ごとにisAttackingを切り替える
        StartCoroutine(RotationRoutine());
    }

    private void Update()
    {
        if (isAttacking)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public void Attack(IDamageable attacker, IDamageable target)
    {
        if (!isAttacking) return;

        // 攻撃の処理
        target.TakeDamageFrom(attacker);

        // 攻撃エフェクトと音の再生
        if (attackEffect != null)
        {
            // Instantiate(attackEffect, attacker.GameObject.transform.position, Quaternion.identity);
        }
        if (attackSound != null)
        {
            /*var soundPlayer = GetComponentInParent<CharacterBase>().soundPlayer;
            soundPlayer.PlaySound(attackSound);*/
        }
    }

    /// <summary>
    /// 一定時間ごとに回転を切り替える
    /// </summary>
    private IEnumerator RotationRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (ShouldRotate())
            {
                character.Rotator.PauseRotation();
                isAttacking = true;
                yield return new WaitForSeconds(attackDuration);
                isAttacking = false;
                character.Rotator.ResumeRotation();
            }
        }
    }

    /// <summary>
    /// 攻撃できるオブジェクトが近くにいるかどうかを判定する
    /// </summary>
    /// <returns>条件を満たすオブジェクトがいる場合はtrue、それ以外はfalse</returns>
    private bool ShouldRotate()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var obj in nearbyObjects)
        {
            if (obj.gameObject.CompareTag(gameObject.tag)) continue;

            IDamageable target = obj.GetComponent<IDamageable>();
            if (target == null) continue;
            return true;
        }
        return false;
    }
}


