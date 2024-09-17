using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// キャラクターを置ける範囲(赤い長方形)を点滅させるスクリプト
/// public classってなんだろう
/// </summary>
public class BlinkingImageScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    private float time;
    private Image blinkingImage;

    void Start()
    {
        blinkingImage = GetComponent<Image>();
    }

    void Update()
    {
        time += Time.deltaTime * 5.0f * speed;

        // 透明度を変更、更新して点滅させる
        Color newColor = GetAlphaColor(blinkingImage.color);
        blinkingImage.color = newColor;
    }

    Color GetAlphaColor(Color color) {
        color.a = (Mathf.Sin(time - Mathf.PI/2) * 0.5f + 0.5f)/2;
        return color;
    }
}
