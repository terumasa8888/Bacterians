using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// �G�𐶐�����X�|�i�[�̃X�N���v�g
/// </summary>
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
        v = transform.position;
        Create();
    }

    /// <summary>
    /// 100�̂̓G�𐶐�����
    /// </summary>
    void Create() {
        for (int i = 0; i < 100; i++) {
            //Enemy(j)��EnemyStand(j)�𐶐�
            var o1 = Instantiate(enemy, v, Quaternion.identity) as GameObject;
            o1.name = enemy.name + "(" + j + ")";
            var o2 = Instantiate(enemyStand, v, Quaternion.identity) as GameObject;
            o2.name = enemyStand.name + "(" + j + ")";

            o1.GetComponent<NavMeshAgent>().speed = speed;//�X�s�[�h������NavMesh���炢����B

            //EnemyStand(es)�̕ϐ�standuser��Enemy(o1)�n������
            EnemyStandScript script = o2.GetComponent<EnemyStandScript>();
            script.standuser = o1;
            script.SetStandStatus(hp, attack);

            j++;
        }
    }
}
