using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    NavMeshAgent enemyNav;
    GameObject playerCore;

    float timer;
    float dis = 0;           //距離保存用
    float nearDis = 100;          //最も近いオブジェクトの距離        
    GameObject targetObj = null; //オブジェクト
    GameObject[] players;

    void Start() {
        enemyNav = GetComponent<NavMeshAgent>();
        playerCore = GameObject.FindWithTag("PlayerCore");

        float x = Random.Range(-0.1f, 0.1f);
        float y = Random.Range(-0.1f, 0.1f);
        float z = Random.Range(-0.1f, 0.1f);
        transform.Translate(x, y, z);

        Search();
    }

    void Update() {

        timer += Time.deltaTime;

        if (timer >= 1) {
            Search();
            timer = 0;

        }
    }

    void Search() {
        players = GameObject.FindGameObjectsWithTag("Player");

        if (enemyNav.pathStatus != NavMeshPathStatus.PathInvalid) {
                if (players.Length != 0) {//いるなら

                    foreach (GameObject obj in players) {
                    //  敵との距離を計算
                        nearDis = 100;
                        dis = Vector3.Distance(obj.transform.position, transform.position);
                        if (nearDis > dis || dis == 0) {
                            nearDis = dis;          //  距離を保存            
                            targetObj = obj;        //  ターゲットを更新
                        }
                    }
                    if (targetObj) {
                        enemyNav.SetDestination(targetObj.transform.position);
                    }
                }
        }
    }
}