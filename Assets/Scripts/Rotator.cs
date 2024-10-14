using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�������_���ɉ�]������X�N���v�g
/// �T�[�N���ɂ����p�ł��邩��
/// UniRx���g���Ă����ƌ����I��
/// </summary>
public class Rotator : MonoBehaviour {

    private float rotationInterval; // ��]�̊Ԋu
    private float maxRotationAngle; // �ő��]�p�x

    private float rotationTimer;
    private float targetRotationZ;
    private float rotationSpeed = 30f; // ��]���x

    private void Start() {
        rotationInterval = Random.Range(2f, 5f); // 2�b����5�b�̊ԂŃ����_���ɐݒ�
        maxRotationAngle = Random.Range(90f, 180f); // 90�x����180�x�̊ԂŃ����_���ɐݒ�
        rotationTimer = rotationInterval;
        targetRotationZ = transform.eulerAngles.z;
    }

    private void Update() {
        HandleRotation();
    }

    private void HandleRotation() {
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <= 0f) {
            float randomAngle = Random.Range(-maxRotationAngle / 2, maxRotationAngle / 2);
            targetRotationZ = transform.eulerAngles.z + randomAngle;
            rotationTimer = rotationInterval;
        }

        // ���݂̉�]�p�x�ƖڕW�̉�]�p�x�̊Ԃ��Ԃ���
        float currentRotationZ = Mathf.LerpAngle(transform.eulerAngles.z, targetRotationZ, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);
    }
}
