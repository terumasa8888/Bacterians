using UnityEngine;

/// <summary>
/// Tilemap�̏��������ɔ�A�N�e�B�u�ɂ���X�N���v�g
/// ��������Ȃ��ƃL�����N�^�[�������Ȃ�
/// </summary>
public class DisableOnStart : MonoBehaviour {
    void Start() {
        gameObject.SetActive(false);
    }
}