using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̃X�^���h�𐧌䂷��X�N���v�g
/// 
/// ���̃N���X�́A�G�L�����N�^�[�̃X�^���h�𐧌䂵�A�X�^���h��HP�A�U���́A���x�Ȃǂ��Ǘ����܂��B
/// �܂��A�X�^���h������̏����𖞂������ꍇ�̓�����`���܂��B
/// </summary>
/// <remarks>
/// �����o�ϐ�:
/// <para><b>GameObject standuser:</b> �X�^���h���[�U�[�̃Q�[���I�u�W�F�N�g</para>
/// <para><b>NavMeshAgent nav:</b> �X�^���h���[�U�[��NavMeshAgent�R���|�[�l���g</para>
/// <para><b>float trueSpeed:</b> �X�^���h���[�U�[�̌��̑��x</para>
/// <para><b>Sprite clioneHealSprite:</b> �N���I�l�̉񕜎��̃X�v���C�g</para>
/// <para><b>Sprite normalSprite:</b> �ʏ펞�̃X�v���C�g</para>
/// <para><b>SpriteRenderer spriteRenderer:</b> �X�v���C�g�����_���[</para>
/// <para><b>float hp:</b> �X�^���h��HP</para>
/// <para><b>float attack:</b> �X�^���h�̍U����</para>
/// <para><b>float speed:</b> �X�^���h�̑��x</para>
/// <para><b>float multiplySpeed:</b> �X�^���h�̑��B���x</para>
/// <para><b>float timer:</b> �^�C�}�[</para>
/// <para><b>float multiplyTimer:</b> ���B�^�C�}�[</para>
/// <para><b>float multiplyLimit:</b> ���B�̐���</para>
/// <para><b>float dis:</b> ����</para>
/// <para><b>GameObject deadEffectPrefab:</b> �X�^���h���j�󂳂ꂽ�ۂɐ�������G�t�F�N�g�̃v���n�u</para>
/// <para><b>GameObject attackEffectPrefab:</b> �X�^���h���U�������ۂɐ�������G�t�F�N�g�̃v���n�u</para>
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
        //������Status�Ŏ����ς�
        if (hp <= 0) {
            Instantiate(deadEffectPrefab, transform.position, Quaternion.identity);
            Destroy(standuser);
            Destroy(this.gameObject);
        }


        //�����̓N���X������ׂ�(���B)
        if(multiplyTimer > 10) {//������5�̓L�������ƂɃC���X�y�N�^�[�ŕς�����悤��
            if (multiplyLimit < 2) {
                multiplyTimer = 0;
                multiplyLimit++;
                //Debug.Log(multiplyLimit);
                var o1 = Instantiate(standuser);
                o1.name = standuser.name + "(Clone)";//�N���[���͓������O
                //var o2 = Instantiate(this.gameObject);

                //o1.GetComponent<NavMeshAgent>().speed = speed;
                //StandScript script = o2.GetComponent<StandScript>();
                //script.SetStandStatus(hp / 2, attack / 2, speed, multiplySpeed);
                //script.standuser = o1;
            }
        }
        //�����ETransform�^��GameObject�^�EGameObject�^��standuser.position�ł͂��߁Bstanduser.transform.position


        /*//������Healable�ȂǂŃN���X������ׂ�
        if (standuser.name.Contains("Clione")) {//�N���I�l�Ȃ�
            if (timer > 5) {
                timer = 0;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                if (players.Length != 0) {//����Ȃ�

                    foreach (GameObject obj in players) {
                        //  �G�Ƃ̋������v�Z
                        dis = Vector3.Distance(obj.transform.position, transform.position);
                        if (dis < 2) {
                            obj.GetComponent<StandScript>().hp += 50;//��
                            Debug.Log("��");
                            //heal();
                            //�񕜂Ȃ�G�t�F�N�g�o����������
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
                Debug.Log("�ǂɂ�������");
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

    void heal() {//�N���I�l�̉񕜎��̃X�v���C�g
        spriteRenderer.sprite = clioneHealSprite;
    }

    void back() {//�N���I�l�ʏ펞�̃X�v���C�g
        spriteRenderer.sprite = normalSprite;
    }*/

}
