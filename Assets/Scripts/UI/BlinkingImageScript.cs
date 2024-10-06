using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �L�����N�^�[��u����͈�(�Ԃ������`)��_�ł�����X�N���v�g
/// public class���ĂȂ񂾂낤
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

        // �����x��ύX�A�X�V���ē_�ł�����
        Color newColor = GetAlphaColor(blinkingImage.color);
        blinkingImage.color = newColor;
    }

    Color GetAlphaColor(Color color) {
        color.a = (Mathf.Sin(time - Mathf.PI/2) * 0.5f + 0.5f)/2;
        return color;
    }
}
