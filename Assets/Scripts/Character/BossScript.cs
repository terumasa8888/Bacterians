using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// stage3のボスのスクリプト
/// 一番近いプレイヤーのキャラに向かってn-way弾を発射する
/// </summary>
public class BossScript : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    float timer = 3;
    public float hp;
    
    public float _Velocity_0, Degree, Angle_Split;

    float _theta;
    float PI = Mathf.PI;
    float rad;

    Vector3 v, v1, v2;
    float dis = 0;
    float nearDis = 100;       
    GameObject targetObj = null; 
    GameObject[] players;

    Rigidbody2D rid2d;

    void Start()
    {
        v1 = transform.rotation.eulerAngles;
        v2 = transform.rotation.eulerAngles;
        rid2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, v2.z);//rigidbody2Dなしで、rotation.xとrotation.yを止める方法。エレガントではないが、Update関数で0を入れ続ける。

        timer += Time.deltaTime;
        if (timer > 3) {
            timer = 0;
            players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length != 0) {

                foreach (GameObject obj in players) {
                    //  敵との距離を計算
                    nearDis = 100;
                    dis = Vector3.Distance(obj.transform.position, transform.position);
                    if (nearDis > dis || dis == 0) {
                        nearDis = dis;          //  距離を保存            
                        targetObj = obj;        //  ターゲットを更新
                    }
                }

                var diff = (targetObj.transform.position - transform.position).normalized;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);
                transform.rotation = Quaternion.FromToRotation(new Vector3(-1, -1, 0), diff);//疑似LoolAt

                v2 = transform.rotation.eulerAngles;
                v = v2 - v1;
                rad = v.z * Mathf.Deg2Rad;//radianにして、これを_thetaに加算



                for (int i = 0; i <= (Angle_Split - 1); i++) {
                    //n-way弾の端から端までの角度
                    float AngleRange = PI * (Degree / 180);

                    //弾インスタンスに渡す角度の計算
                    if (Angle_Split > 1) _theta = (AngleRange / (Angle_Split - 1)) * i + 0.5f * (PI - AngleRange);
                    else _theta = 0.5f * PI;

                    //弾インスタンスを取得し、初速と発射角度を与える
                    GameObject Bullet_obj = (GameObject)Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
                    EnemyBulletScript enemyBulletScript = Bullet_obj.GetComponent<EnemyBulletScript>();
                    enemyBulletScript.theta = _theta + rad;//rad追加
                    enemyBulletScript.Velocity_0 = _Velocity_0;
                }
            }

        }


        if(hp <= 0) {
            Destroy(gameObject);
        }

    }
}
