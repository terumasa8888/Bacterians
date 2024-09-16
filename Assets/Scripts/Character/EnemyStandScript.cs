using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̃X�^���h(�킹����)�̃X�N���v�g
/// 
/// </summary>
public class EnemyStandScript : MonoBehaviour {

    public GameObject standuser;//�X�^���h��킹��I�u�W�F�N�g
    NavMeshAgent nav;
    float trueSpeed;//�^�̃X�s�[�h��ۑ�

    public float hp, attack;//�X�e�[�^�X
    float timer;//�U���Ԋu�p

    public GameObject deadEffectPrefab;//���S�G�t�F�N�g

    void Start() {
        nav = standuser.GetComponent<NavMeshAgent>();
        trueSpeed = nav.speed;//nav.Speed��standuser�̃X�s�[�h
    }

    void Update() {
        this.transform.position = standuser.transform.position;
        timer += Time.deltaTime;

        if (hp <= 0) {
            Instantiate(deadEffectPrefab, transform.position, Quaternion.identity);//���S�G�t�F�N�g����
            Destroy(standuser);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //�v���C���[�ɓ���������
        if (collision.gameObject.CompareTag("Player")) {
            if (timer > 2) {
                timer = 0;
                Stop();
                collision.gameObject.GetComponent<StandScript>().hp -= attack;

                Invoke("Go", 2);//2�b���Go()���Ă�
            }
        }
        //�v���C���[�̃R�A�ɓ���������
        if(collision.gameObject.CompareTag("PlayerCore")) {
            if (timer > 2) {
                timer = 0;
                Stop();
                collision.gameObject.GetComponent<PlayerCoreScript>().hp -= attack;

                Invoke("Go", 2);
            }
        }

    }

    public void SetStandStatus(float hp, float attack) {//�Z�b�^�[
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
