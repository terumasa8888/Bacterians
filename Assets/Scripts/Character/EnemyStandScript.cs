using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyStandScript : MonoBehaviour {

    public GameObject standuser;
    NavMeshAgent nav;
    float trueSpeed;

    public float hp, attack;
    float timer;

    public GameObject deadEffectPrefab;

    void Start() {
        nav = standuser.GetComponent<NavMeshAgent>();//キャッシュ
        trueSpeed = nav.speed;//2。真の値を保存
    }

    void Update() {
        this.transform.position = standuser.transform.position;
        timer += Time.deltaTime;

        if (hp <= 0) {
            Instantiate(deadEffectPrefab, transform.position, Quaternion.identity);
            Destroy(standuser);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (timer > 2) {
                timer = 0;
                Stop();
                collision.gameObject.GetComponent<StandScript>().hp -= attack;

                Invoke("Go", 2);
            }
        }

        if(collision.gameObject.CompareTag("PlayerCore")) {
            if (timer > 2) {
                timer = 0;
                Stop();
                collision.gameObject.GetComponent<PlayerCoreScript>().hp -= attack;

                Invoke("Go", 2);
            }
        }

    }

    public void SetStandStatus(float hp, float attack) {//セッター
        this.hp = hp;
        this.attack = attack;
    }
    void Stop() {
        nav.speed = 0;
    }

    void Go() {
        nav.speed = trueSpeed;
    }
}
