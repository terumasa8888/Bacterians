using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    NavMeshAgent enemyNav;
    GameObject playerCore;

    float timer;
    float dis = 0;           //�����ۑ��p
    float nearDis = 100;          //�ł��߂��I�u�W�F�N�g�̋���        
    GameObject targetObj = null; //�I�u�W�F�N�g
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
                if (players.Length != 0) {//����Ȃ�

                    foreach (GameObject obj in players) {
                    //  �G�Ƃ̋������v�Z
                        nearDis = 100;
                        dis = Vector3.Distance(obj.transform.position, transform.position);
                        if (nearDis > dis || dis == 0) {
                            nearDis = dis;          //  ������ۑ�            
                            targetObj = obj;        //  �^�[�Q�b�g���X�V
                        }
                    }
                    if (targetObj) {
                        enemyNav.SetDestination(targetObj.transform.position);
                    }
                }
        }
    }
}