using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// クリア音と花火を生成するスクリプト
/// </summary>
public class ClearEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject fireWorkPrefab;
    [SerializeField] private GameObject clearSound;

    void Start()
    {
        Instantiate(clearSound);

        Observable.Interval(System.TimeSpan.FromSeconds(0.5))
            .Subscribe(_ => SpawnFirework())
            .AddTo(this);
    }

    private void SpawnFirework()
    {
        float x = Random.Range(-10, 10);
        float y = Random.Range(-5, 5);
        Vector3 position = new Vector3(x, y, -8);
        Instantiate(fireWorkPrefab, position, Quaternion.identity);
    }
}
