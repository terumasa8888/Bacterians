using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトをランダムに回転させるスクリプト
/// サークルにも応用できるかも
/// UniRxを使ってもっと効率的に
/// </summary>
public class Rotator : MonoBehaviour {

    private float rotationInterval; // 回転の間隔
    private float maxRotationAngle; // 最大回転角度

    private float rotationTimer;
    private float targetRotationZ;
    private float rotationSpeed = 30f; // 回転速度

    private void Start() {
        rotationInterval = Random.Range(2f, 5f); // 2秒から5秒の間でランダムに設定
        maxRotationAngle = Random.Range(90f, 180f); // 90度から180度の間でランダムに設定
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

        // 現在の回転角度と目標の回転角度の間を補間する
        float currentRotationZ = Mathf.LerpAngle(transform.eulerAngles.z, targetRotationZ, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);
    }
}
