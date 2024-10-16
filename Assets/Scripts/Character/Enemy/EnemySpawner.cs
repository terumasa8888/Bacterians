using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 敵を生成するスポナーのスクリプト
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemyCount = 50;


    void Start()
    {
        CreateEnemy();
    }

    /// <summary>
    /// Enemyを生成する
    /// </summary>
    private void CreateEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject clone = Instantiate(enemy, transform.position, Quaternion.identity);
            ScatterPosition(clone);
            clone.name = enemy.name + "(" + (i + 1) + ")";
        }
    }

    /// <summary>
    /// キャラクターの生成位置を散らす
    /// PlayerSpawnerScriptにも近い処理がある
    /// </summary>
    private void ScatterPosition(GameObject character)
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        character.transform.position += new Vector3(x, y, 0);
    }
}
