using UnityEngine;
using System.Collections;

/// <summary>
/// オブジェクトをランダムに回転させるロジックを提供するクラス
/// </summary>
public class RotatorLogic
{
    private float rotationInterval;
    private float maxRotationAngle;
    private float rotationTimer;
    private float targetRotationZ;
    private float rotationSpeed = 30f;
    private Transform transform;
    private bool isPaused = false;

    public RotatorLogic(Transform transform)
    {
        this.transform = transform;
        InitializeRotation();
    }

    private void InitializeRotation()
    {
        rotationInterval = Random.Range(2f, 5f);
        maxRotationAngle = Random.Range(90f, 180f);
        rotationTimer = rotationInterval;
        targetRotationZ = transform.eulerAngles.z;
    }

    public IEnumerator UpdateRotation()
    {
        while (true)
        {
            if (!isPaused)
            {
                rotationTimer -= Time.deltaTime;
                if (rotationTimer <= 0f)
                {
                    float randomAngle = Random.Range(-maxRotationAngle / 2, maxRotationAngle / 2);
                    targetRotationZ = transform.eulerAngles.z + randomAngle;
                    rotationTimer = rotationInterval;
                }

                float currentRotationZ = Mathf.LerpAngle(transform.eulerAngles.z, targetRotationZ, Time.deltaTime * rotationSpeed);
                transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);
            }
            yield return null;
        }
    }

    public void PauseRotation()
    {
        isPaused = true;
    }

    public void ResumeRotation()
    {
        isPaused = false;
    }
}




