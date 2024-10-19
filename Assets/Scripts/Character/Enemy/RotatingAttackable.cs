using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 回転しながら、触れた敵にダメージを与えるコンポーネント
/// </summary>
public class RotatingAttackable : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private float moveDistance = 3f; // 移動距離
    [SerializeField] private float moveDuration = 3f; // 移動時間
    [SerializeField] private float detectionRadius = 3f;

    private int attackPower;
    private float moveSpeed;
    private string cachedTag;
    private bool isMoving = false;
    private GameObject[] players;
    private Vector3 moveDirection;
    private Vector3 startPosition;

    void Start()
    {
        attackPower = GetComponent<Status>().Attack;
        cachedTag = gameObject.tag;

        // 攻撃ルーチン
        Observable.Interval(System.TimeSpan.FromSeconds(waitTime))
            .Where(_ => !isMoving)
            .Select(_ => FindNearestPlayer())
            .Where(player => player != null)
            .Subscribe(player => MoveTowards(player))
            .AddTo(this);
    }

    void Update()
    {
        if (!isMoving) return;

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.World);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
        {
            isMoving = false;
        }
    }

    /// <summary>
    /// 衝突した相手にダメージを与える
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMoving) return;

        GameObject collidedObject = collision.gameObject;
        var damageable = collidedObject.GetComponent<IDamageable>();

        if (damageable == null || cachedTag == collidedObject.tag) return;
        damageable.TakeDamage(attackPower, cachedTag);
    }

    /// <summary>
    /// isMovingを更新することでプレイヤーに向かって移動
    /// </summary>
    private void MoveTowards(GameObject player)
    {
        moveDirection = (player.transform.position - transform.position).normalized;
        startPosition = transform.position;
        moveSpeed = moveDistance / moveDuration;
        isMoving = true;

        Observable.Timer(System.TimeSpan.FromSeconds(moveDuration))
            .Subscribe(_ =>
            {
                isMoving = false;
            })
            .AddTo(this);
    }

    /// <summary>
    /// 一番近いプレイヤーを探す
    /// </summary>
    private GameObject FindNearestPlayer()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearestPlayer = null;
        float minDistance = detectionRadius;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= minDistance)
            {
                minDistance = distance;
                nearestPlayer = player;
            }
        }

        return nearestPlayer;
    }
}
