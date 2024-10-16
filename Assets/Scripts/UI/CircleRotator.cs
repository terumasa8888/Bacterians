using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotator : MonoBehaviour
{
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(1, Vector3.back);
    }
}
