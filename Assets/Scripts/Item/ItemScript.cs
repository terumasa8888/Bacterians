using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���̃X�N���v�g
/// hp��0�ɂȂ�ƁA�v���C���[�̍U���͂�3�{�ɂ���
/// �U���͂𒼐ڂ������Ă�̂��悭�Ȃ�
/// �J�v�Z��������ׂ�
/// </summary>
public class ItemScript : MonoBehaviour {

    public float hp;

    void Start() {

    }

    void Update() {
        //Debug.Log(hp);
        if (hp <= 0) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in players) {
                obj.GetComponent<StandScript>().attack *= 3;
            }
            Destroy(this.gameObject);
            Debug.Log("�A�C�e�����󂵂���");
        }
    }
}
