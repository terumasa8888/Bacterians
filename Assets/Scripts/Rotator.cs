using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    private float rotationInterval; // ‰ñ“]‚ÌŠÔŠu
    private float maxRotationAngle; // Å‘å‰ñ“]Šp“x

    private float rotationTimer;
    private float targetRotationZ;
    private float rotationSpeed = 30f; // ‰ñ“]‘¬“x

    private void Start() {
        rotationInterval = Random.Range(2f, 5f); // 2•b‚©‚ç5•b‚ÌŠÔ‚Åƒ‰ƒ“ƒ_ƒ€‚Éİ’è
        maxRotationAngle = Random.Range(90f, 180f); // 90“x‚©‚ç180“x‚ÌŠÔ‚Åƒ‰ƒ“ƒ_ƒ€‚Éİ’è
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

        // Œ»İ‚Ì‰ñ“]Šp“x‚Æ–Ú•W‚Ì‰ñ“]Šp“x‚ÌŠÔ‚ğ•âŠÔ‚·‚é
        float currentRotationZ = Mathf.LerpAngle(transform.eulerAngles.z, targetRotationZ, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);
    }
}
