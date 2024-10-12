using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// アイテムを生成するスクリプト
/// アイテムを壊したキャラクターの攻撃力を増加させる処理も行っている
/// </summary>
public class ItemSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private float x_Max, x_Min, y_Max, y_Min;
    [SerializeField] private float instantiateTime;
    private const int attackMultiplier = 3; // 攻撃力の倍率を設定

    void Start()
    {
        StartCoroutine(InstantiateItemAfterDelay(instantiateTime));
        MessageBroker.Default.Receive<ItemDestroyedMessage>() // ここがUniRxのMessageBroker機能
            .Subscribe(message => MultiplyAttack(message.AttackerTag)) // ここがUniRxのSubscribe機能
            .AddTo(this); // メモリリークを防ぐためのAddTo
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
        Debug.Log($"{attackerTag}の攻撃力を{attackMultiplier}倍にしたよ");
    }
}
