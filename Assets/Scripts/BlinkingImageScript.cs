using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingImageScript : MonoBehaviour
{
    public Image blinkingImage;
    public float speed = 1.0f;
    private float time;

    void Start()
    {
        //speed = 0.2f;
        blinkingImage = GetComponent<Image>();
    }

    void Update()
    {
        blinkingImage.color = GetAlphaColor(blinkingImage.color);
    }

    Color GetAlphaColor(Color color) {
        time += Time.deltaTime * 5.0f * speed;
        color.a = (Mathf.Sin(time - Mathf.PI/2) * 0.5f + 0.5f)/2;

        return color;
    }
}
