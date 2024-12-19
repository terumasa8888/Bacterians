using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�[�Y�{�^�����������Ƃ��̏���
/// Button�֘A�̏����Ȃ̂ɖ��O�̓��ꊴ���Ȃ�
/// </summary>
public class PauseScript : MonoBehaviour {

    /*[SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject reStartButton;*/
    [SerializeField]
    private GameObject pausePanel;

    public void StopGame() {
        Time.timeScale = 0f;
        //pauseButton.SetActive(false);
        //reStartButton.SetActive(true);
        pausePanel.SetActive(true);
        //pausePanel�̕\����肾���Ǘ�����ׂ�
    }

    public void ReStartGame() {
        pausePanel.SetActive(false);
        //reStartButton.SetActive(false);
        //pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
