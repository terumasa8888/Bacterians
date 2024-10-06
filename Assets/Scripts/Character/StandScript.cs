using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵のスタンドを制御するスクリプト
/// 
/// このクラスは、敵キャラクターのスタンドを制御し、スタンドのHP、攻撃力、速度などを管理します。
/// また、スタンドが特定の条件を満たした場合の動作を定義します。
/// </summary>
/// <remarks>
/// メンバ変数:
/// <para><b>GameObject standuser:</b> スタンドユーザーのゲームオブジェクト</para>
/// <para><b>NavMeshAgent nav:</b> スタンドユーザーのNavMeshAgentコンポーネント</para>
/// <para><b>float trueSpeed:</b> スタンドユーザーの元の速度</para>
/// <para><b>Sprite clioneHealSprite:</b> クリオネの回復時のスプライト</para>
/// <para><b>Sprite normalSprite:</b> 通常時のスプライト</para>
/// <para><b>SpriteRenderer spriteRenderer:</b> スプライトレンダラー</para>
/// <para><b>float hp:</b> スタンドのHP</para>
/// <para><b>float attack:</b> スタンドの攻撃力</para>
/// <para><b>float speed:</b> スタンドの速度</para>
/// <para><b>float multiplySpeed:</b> スタンドの増殖速度</para>
/// <para><b>float timer:</b> タイマー</para>
/// <para><b>float multiplyTimer:</b> 増殖タイマー</para>
/// <para><b>float multiplyLimit:</b> 増殖の制限</para>
/// <para><b>float dis:</b> 距離</para>
/// <para><b>GameObject deadEffectPrefab:</b> スタンドが破壊された際に生成するエフェクトのプレハブ</para>
/// <para><b>GameObject attackEffectPrefab:</b> スタンドが攻撃した際に生成するエフェクトのプレハブ</para>
/// </remarks>
public class StandScript : MonoBehaviour
{
    public GameObject standuser;
    NavMeshAgent nav;
    float  trueSpeed;
    public Sprite clioneHealSprite;
    Sprite normalSprite;
    SpriteRenderer spriteRenderer;

    public float hp, attack, speed, multiplySpeed;
    float timer = 0;
    float multiplyTimer = 0;
    float multiplyLimit = 0;

    float dis = 0;

    public GameObject deadEffectPrefab;
    public GameObject attackEffectPrefab;

    void Start()
    {
        nav = standuser.GetComponent<NavMeshAgent>();
        trueSpeed = nav.speed;
        normalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();


        if (this.gameObject.name.Contains("Clone")) {
            multiplyLimit = 2;
        }
    }

    void Update()
    {
        this.transform.position = standuser.transform.position;
        timer += Time.deltaTime;
        multiplyTimer += Time.deltaTime;
        //ここはStatusで実装済み
        if (hp <= 0) {
            Instantiate(deadEffectPrefab, transform.position, Quaternion.identity);
            Destroy(standuser);
            Destroy(this.gameObject);
        }


        //ここはクラス分けるべき(増殖)
        if(multiplyTimer > 10) {//ここの5はキャラごとにインスペクターで変えられるように
            if (multiplyLimit < 2) {
                multiplyTimer = 0;
                multiplyLimit++;
                //Debug.Log(multiplyLimit);
                var o1 = Instantiate(standuser);
                o1.name = standuser.name + "(Clone)";//クローンは同じ名前
                //var o2 = Instantiate(this.gameObject);

                //o1.GetComponent<NavMeshAgent>().speed = speed;
                //StandScript script = o2.GetComponent<StandScript>();
                //script.SetStandStatus(hp / 2, attack / 2, speed, multiplySpeed);
                //script.standuser = o1;
            }
        }
        //メモ・Transform型とGameObject型・GameObject型はstanduser.positionではだめ。standuser.transform.position


        /*//ここはHealableなどでクラス分けるべき
        if (standuser.name.Contains("Clione")) {//クリオネなら
            if (timer > 5) {
                timer = 0;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                if (players.Length != 0) {//いるなら

                    foreach (GameObject obj in players) {
                        //  敵との距離を計算
                        dis = Vector3.Distance(obj.transform.position, transform.position);
                        if (dis < 2) {
                            obj.GetComponent<StandScript>().hp += 50;//回復
                            Debug.Log("回復");
                            //heal();
                            //回復ならエフェクト出す方がいい
                            Invoke("back", 2);
                        }
                    }
                }
            }
        }*/
    }

    /*private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Enemy")) {
            if (timer > 2) {
                timer = 0;
                Stop();
                //collision.gameObject.GetComponent<EnemyStandScript>().hp -= attack;

                Invoke("Go", 2);
            }
        }

        if (collision.gameObject.CompareTag("Boss")) {
            if (timer > 2) {
                timer = 0;
                Stop();
                collision.gameObject.GetComponent<BossScript>().hp -= attack;
                Instantiate(attackEffectPrefab, transform.position, Quaternion.identity);

                Invoke("Go", 2);
            }
        }

        if (collision.gameObject.CompareTag("EnemyBullet")) {
            Instantiate(collision.gameObject.GetComponent<EnemyBulletScript>().hitEffectPrefab, transform.position, Quaternion.identity);
            //Destroy(collision.gameObject);
            hp = 0;
        }

        if (collision.gameObject.CompareTag("Item")) {
            collision.gameObject.GetComponent<ItemScript>().hp -= attack;
        }
        if (collision.gameObject.CompareTag("Wall")) {
            //if (timer > 2) {
                Debug.Log("壁にあたった");
                Stop();
            //}
        }
    }*/

    /*public void SetStandStatus(float hp, float attack, float speed, float multiplySpeed) {
        this.hp = hp;
        this.attack = attack;
        this.speed = speed;
        this.multiplySpeed = multiplySpeed;
    }

    void Stop() {
        nav.speed = 0;
    }

    void Go() {
        nav.speed = trueSpeed;
    }

    void heal() {//クリオネの回復時のスプライト
        spriteRenderer.sprite = clioneHealSprite;
    }

    void back() {//クリオネ通常時のスプライト
        spriteRenderer.sprite = normalSprite;
    }*/

}
