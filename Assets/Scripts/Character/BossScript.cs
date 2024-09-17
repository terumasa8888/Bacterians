using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// stage3�̃{�X�̃X�N���v�g
/// ��ԋ߂��v���C���[�̃L�����Ɍ�������n-way�e�𔭎˂���
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
        transform.eulerAngles = new Vector3(0, 0, v2.z);//rigidbody2D�Ȃ��ŁArotation.x��rotation.y���~�߂���@�B�G���K���g�ł͂Ȃ����AUpdate�֐���0����ꑱ����B

        timer += Time.deltaTime;
        if (timer > 3) {
            timer = 0;
            players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length != 0) {

                foreach (GameObject obj in players) {
                    //  �G�Ƃ̋������v�Z
                    nearDis = 100;
                    dis = Vector3.Distance(obj.transform.position, transform.position);
                    if (nearDis > dis || dis == 0) {
                        nearDis = dis;          //  ������ۑ�            
                        targetObj = obj;        //  �^�[�Q�b�g���X�V
                    }
                }

                var diff = (targetObj.transform.position - transform.position).normalized;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);
                transform.rotation = Quaternion.FromToRotation(new Vector3(-1, -1, 0), diff);//�^��LoolAt

                v2 = transform.rotation.eulerAngles;
                v = v2 - v1;
                rad = v.z * Mathf.Deg2Rad;//radian�ɂ��āA�����_theta�ɉ��Z



                for (int i = 0; i <= (Angle_Split - 1); i++) {
                    //n-way�e�̒[����[�܂ł̊p�x
                    float AngleRange = PI * (Degree / 180);

                    //�e�C���X�^���X�ɓn���p�x�̌v�Z
                    if (Angle_Split > 1) _theta = (AngleRange / (Angle_Split - 1)) * i + 0.5f * (PI - AngleRange);
                    else _theta = 0.5f * PI;

                    //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
                    GameObject Bullet_obj = (GameObject)Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
                    EnemyBulletScript enemyBulletScript = Bullet_obj.GetComponent<EnemyBulletScript>();
                    enemyBulletScript.theta = _theta + rad;//rad�ǉ�
                    enemyBulletScript.Velocity_0 = _Velocity_0;
                }
            }

        }


        if(hp <= 0) {
            Destroy(gameObject);
        }

    }
}
