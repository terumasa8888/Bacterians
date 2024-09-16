using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyStand;
    Vector3 v;
    float timer;
    int j = 1;
    public float hp, attack, speed;

    void Start()
    {
        GameObject enemyCore = GameObject.FindWithTag("EnemyCore");
        //v = enemyCore.transform.position;
        v = transform.position;
        //Debug.Log(v);

        Create();
    }

    /*void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5) {
            Create();
            timer = 0;

        }
    }*/

    /// <summary>
    /// 100体の敵を生成する
    /// </summary>
    void Create() {
        for (int i = 0; i < 100; i++) {
            //Enemy(j)とEnemyStand(j)を生成
            var o1 = Instantiate(enemy, v, Quaternion.identity) as GameObject;
            o1.name = enemy.name + "(" + j + ")";
            var o2 = Instantiate(enemyStand, v, Quaternion.identity) as GameObject;
            o2.name = enemyStand.name + "(" + j + ")";

            o1.GetComponent<NavMeshAgent>().speed = speed;//スピードだけはNavMeshからいじる。

            //EnemyStand(es)の変数standuserにEnemy(o1)渡したい
            EnemyStandScript script = o2.GetComponent<EnemyStandScript>();
            script.standuser = o1;
            script.SetStandStatus(hp, attack);

            j++;
        }
    }
}
