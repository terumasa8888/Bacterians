using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトをランダムに回転させる
/// </summary>
public class Rotator : MonoBehaviour {

    private float rotationInterval;
    private float maxRotationAngle;

    private float rotationTimer;
    private float targetRotationZ;
    private float rotationSpeed = 30f;

    private void Start() {
        rotationInterval = Random.Range(2f, 5f); 
        maxRotationAngle = Random.Range(90f, 180f);
        rotationTimer = rotationInterval;
        targetRotationZ = transform.eulerAngles.z;
    }

    private void Update() {
        HandleRotation();
    }

    /// <summary>
    /// オブジェクトをランダムに回転させる
    /// </summary>
    private void HandleRotation() {
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <= 0f) {
            float randomAngle = Random.Range(-maxRotationAngle / 2, maxRotationAngle / 2);
            targetRotationZ = transform.eulerAngles.z + randomAngle;
            rotationTimer = rotationInterval;
        }

        float currentRotationZ = Mathf.LerpAngle(transform.eulerAngles.z, targetRotationZ, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);
    }
}
