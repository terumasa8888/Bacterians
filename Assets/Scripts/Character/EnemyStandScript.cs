using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵のスタンド(被せもの)のスクリプト
/// 
/// </summary>
public class EnemyStandScript : MonoBehaviour {

    public GameObject standuser;//スタンドを被せるオブジェクト
    NavMeshAgent nav;
    float trueSpeed;//真のスピードを保存

    public float hp, attack;//ステータス
    float timer;//攻撃間隔用

    public GameObject deadEffectPrefab;//死亡エフェクト

    void Start() {
        nav = standuser.GetComponent<NavMeshAgent>();
        trueSpeed = nav.speed;//nav.Speedはstanduserのスピード
    }

    void Update() {
        this.transform.position = standuser.transform.position;
        timer += Time.deltaTime;

        if (hp <= 0) {
            Instantiate(deadEffectPrefab, transform.position, Quaternion.identity);//死亡エフェクト発生
            Destroy(standuser);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //プレイヤーに当たった時
        if (collision.gameObject.CompareTag("Player")) {
            if (timer > 2) {
                timer = 0;
                Stop();
                collision.gameObject.GetComponent<StandScript>().hp -= attack;

                Invoke("Go", 2);//2秒後にGo()を呼ぶ
            }
        }
        //プレイヤーのコアに当たった時
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
