using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// アイテムを生成するスクリプト
/// アイテムを壊したキャラクターの攻撃力を増加させる処理も行っているのはおかしい
/// </summary>
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private float x_Max, x_Min, y_Max, y_Min;
    [SerializeField] private float instantiateTime;
    private const int attackMultiplier = 3; // 攻撃力の倍率

    void Start()
    {
        StartCoroutine(InstantiateItemAfterDelay(instantiateTime));
        MessageBroker.Default.Receive<ItemDestroyedMessage>()
            .Subscribe(message => MultiplyAttack(message.AttackerTag))
            .AddTo(this);
    }

    IEnumerator InstantiateItemAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(item, new Vector3(Random.Range(x_Min, x_Max), Random.Range(y_Min, y_Max), -1), Quaternion.identity);
    }

    void MultiplyAttack(string attackerTag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(attackerTag);
        foreach (GameObject target in targets)
        {
            target.GetComponent<Status>().MultiplyAttack(attackMultiplier);
        }
    }
}
