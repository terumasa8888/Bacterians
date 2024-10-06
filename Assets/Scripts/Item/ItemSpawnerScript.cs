using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// アイテムを生成するスクリプト
/// </summary>
public class ItemSpawnerScript : MonoBehaviour
{
    public GameObject item;//prefab
    public float x_Max, x_Min, y_Max, y_Min;
    public float instantiateTime;

    void Start()
    {
        Invoke("InstantiateItem", instantiateTime);
    }

    void Update()
    {
        
    }

    void InstantiateItem() {
        Instantiate(item, new Vector3(Random.Range(x_Min, x_Max), Random.Range(y_Min, y_Max), -1), Quaternion.identity);
    }
}
