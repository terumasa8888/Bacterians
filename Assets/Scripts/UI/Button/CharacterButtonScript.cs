using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �e�L�����N�^�[�{�^���̃X�N���v�g
/// CharacterButtonScript�Ƃ������O�Ȃ̂�
/// �T�[�N���̕\���E�ʒu�ύX�܂ł���Ă��܂��Ă���
/// OnClick�͖��O������킵��
/// OnButtonClicked�����݂���̂ł������ɕύX���ׂ��H
/// �������A���O���ς�邽�т�Inspector�̐ݒ���ύX���Ȃ��Ƃ����Ȃ�
/// �܂�A�R�[�h���玩���I��OnButtonClicked�����s����ق����悢
/// (Inspector������s���\�b�h��ݒ肵�Ȃ��čς�)
/// IsClicked�������Ɨǂ����O�ɕύX���ׂ�
/// IsSelected�Ƃ�
/// </summary>
public class CharacterButtonScript : MonoBehaviour
{
    private bool isClicked = false;

    private GameObject buttonManager;
    ButtonManagerScript buttonManagerScript;

    void Start()
    {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();
    }

    public void OnClick() {
        if (isClicked) {//���łɃ{�^��������Ă���Ȃ�
            isClicked = false;
            //�{�^���T�[�N��(��)���\���ɂ���
            buttonManagerScript.buttonCircle.SetActive(false);
            //�T�[�N��(��)���\���ɂ���
            buttonManagerScript.circle.SetActive(false);
        }
        else {//������ĂȂ��Ȃ�
            buttonManagerScript.ResetOther();
            isClicked = true;
            buttonManagerScript.buttonCircle.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            buttonManagerScript.buttonCircle.SetActive(true);
        }
    }

    public bool IsClicked() {
        return isClicked;
    }

    /// <summary>
    /// �{�^���̃N���b�N��Ԃ����Z�b�g����
    /// </summary>
    public void ResetClickState() {
        isClicked = false;
    }


}
