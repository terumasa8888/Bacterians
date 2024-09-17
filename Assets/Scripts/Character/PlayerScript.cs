using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// �v���C���[�L�����N�^�[�̓���𐧌䂷��X�N���v�g�B
/// �i�r���b�V���G�[�W�F���g���g�p���āA�v���C���[��ړI�n�Ɉړ������܂��B
/// </summary>
public class PlayerScript : MonoBehaviour {
    NavMeshAgent playerNav;
    public Vector3 mousePosition;
    public bool distinationFlag = true;

    void Start() {
        playerNav = GetComponent<NavMeshAgent>();

        float x = Random.Range(-0.1f, 0.1f);
        float y = Random.Range(-0.1f, 0.1f);
        float z = Random.Range(-0.1f, 0.1f);
        transform.Translate(x, y, z);

        if (playerNav.pathStatus != NavMeshPathStatus.PathInvalid) {
            playerNav.SetDestination(mousePosition);
        }
    }

    void FixedUpdate() {
        if (playerNav.pathStatus != NavMeshPathStatus.PathInvalid) {
            playerNav.SetDestination(mousePosition);
        }
    }
}
