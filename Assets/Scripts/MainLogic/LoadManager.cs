using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V�[���J�ڎ���TimeScale��1�ɂ���
/// �V�[���J�ڎ���TimeScale���Ȃ���0�̂܂܂ɂȂ邽��
/// ����1.�O�̃V�[���� Time.timeScale �� 0 �ɐݒ肳��Ă���H
/// ����2.�񓯊��V�[�����[�h�iSceneManager.LoadSceneAsync�j���g�p���Ă���
/// </summary>

public class LoadManager : MonoBehaviour
{
    void Start() {
        Time.timeScale = 1f;
    }
}
