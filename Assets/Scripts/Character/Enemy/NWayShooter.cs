using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Pool;

/// <summary>
/// 一番近いキャラに向かってn-way弾を発射する 
/// </summary>
public class NWayShooter : MonoBehaviour
{
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private float velocity;
    [SerializeField] private float degreeRange;
    [SerializeField] private int n;
    [SerializeField] private float fireInterval = 3f;
    [SerializeField][Tag] private string targetTag;

    private float targetAngle;
    private GameObject targetObject = null;
    float pi = Mathf.PI;

    private ObjectPool<GameObject> bulletPool;

    void Start()
    {
        // 弾のプールを作成
        bulletPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(enemyBulletPrefab),
            actionOnGet: bullet => bullet.SetActive(true),
            actionOnRelease: bullet =>
            {
                if (bullet == null) return;
                bullet.SetActive(false);
            },
            actionOnDestroy: bullet =>
            {
                if (bullet == null) return;
                Destroy(bullet);
            },
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 100
        );

        // 一定間隔で弾を発射
        Observable.Interval(TimeSpan.FromSeconds(fireInterval))
            .Subscribe(_ =>
            {
                FindNearestPlayer();
                if (targetObject == null) return;
                RotateTowardsTarget();
                FireNWayBullets();
            })
            .AddTo(this);
    }

    /// <summary>
    /// 一番近いプレイヤーを探す
    /// </summary>
    private void FindNearestPlayer()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        float nearestDistance = float.MaxValue;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance >= nearestDistance) continue;
            nearestDistance = distance;
            targetObject = target;
        }
    }

    /// <summary>
    /// ターゲットの方向に向く
    /// </summary>
    private void RotateTowardsTarget()
    {
        Vector3 direction = (targetObject.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(new Vector3(-1, -1, 0), direction);
        Vector3 baseDirection = new Vector3(-1, -1, 0).normalized;
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x);
        targetAngle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad + baseAngle;
    }

    /// <summary>
    /// n-way弾を発射する
    /// </summary>
    private void FireNWayBullets()
    {
        for (int i = 0; i < n; i++)
        {
            float radianRange = pi * (degreeRange / 180);
            float bulletAngleOffset = (n > 1) ? (radianRange / (n - 1)) * i - 0.5f * radianRange : 0.0f;

            GameObject bullet = bulletPool.Get();
            if (bullet == null) continue;

            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            EnemyBulletScript enemyBulletScript = bullet.GetComponent<EnemyBulletScript>();
            if (enemyBulletScript == null) continue;

            enemyBulletScript.Initialize(velocity, bulletAngleOffset + targetAngle);
            enemyBulletScript.SetPool(bulletPool);
        }
    }
}
