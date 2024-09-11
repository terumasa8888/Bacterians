using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour {
    NavMeshAgent playerNav;
    GameObject enemyCore;
    public Vector3 mousePosition;
    public bool distinationFlag = true;
    /*
    int number = 0;

    float timer;
    float dis = 0;           //距離保存用
    float nearDis = 100;          //最も近いオブジェクトの距離        
    GameObject targetObj = null; //オブジェクト
    GameObject[] enemies;

    GameObject item;
    */

    void Start() {
        playerNav = GetComponent<NavMeshAgent>();
        enemyCore = GameObject.FindWithTag("EnemyCore");

        float x = Random.Range(-0.1f, 0.1f);
        float y = Random.Range(-0.1f, 0.1f);
        float z = Random.Range(-0.1f, 0.1f);
        transform.Translate(x, y, z);

        /*if (this.gameObject.name.Contains("HouseDust")) {
            number = 2;
            Search();
        }
        else if (this.gameObject.name.Contains("Pirori")) {
            number = 5;
            item = GameObject.FindWithTag("Item");
        }*/


        //else {
        if (playerNav.pathStatus != NavMeshPathStatus.PathInvalid) {
            if (distinationFlag) {
                    //playerNav.SetDestination(enemyCore.transform.position);
                    playerNav.SetDestination(mousePosition);
                    //Debug.Log(mousePosition);
            }
        }

        //}

    }

    void FixedUpdate() {
        /*if (number == 2) {
            Search();
        }
        else if (number == 5) {
            if (playerNav.pathStatus != NavMeshPathStatus.PathInvalid) {
                if (item) {
                    playerNav.SetDestination(item.transform.position);
                }
                else {
                    if (distinationFlag) {
                        playerNav.SetDestination(enemyCore.transform.position);
                    }
                    else {
                        playerNav.SetDestination(mousePosition);
                    }
                }
            }
        }*/


        //else {
            if (playerNav.pathStatus != NavMeshPathStatus.PathInvalid) {
                if (distinationFlag) {
                    if (enemyCore != null) {
                        playerNav.SetDestination(enemyCore.transform.position);
                    }
                }
                else {
                    playerNav.SetDestination(mousePosition);
                }
            }
        //}




    }

    /*void Search() {//ホコリ
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (playerNav.pathStatus != NavMeshPathStatus.PathInvalid) {
            if (distinationFlag) {
                if (enemies.Length != 0) {//Enemyいるなら

                    foreach (GameObject obj in enemies) {
                        //  敵との距離を計算
                        dis = Vector3.Distance(obj.transform.position, transform.position);
                        if (nearDis > dis || dis == 0) {
                            nearDis = dis;          //  距離を保存            
                            targetObj = obj;        //  ターゲットを更新
                        }
                    }
                    if (targetObj) {
                        playerNav.SetDestination(targetObj.transform.position);
                    }
                }
                else {//Enemyいないなら
                    playerNav.SetDestination(enemyCore.transform.position);
                }
            }
            else {//flag==false
                    playerNav.SetDestination(mousePosition);
            }
        }
    }*/
}
