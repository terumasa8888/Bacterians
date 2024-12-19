using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズボタンを押したときの処理
/// Button関連の処理なのに名前の統一感がない
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
        //pausePanelの表示回りだけ管理するべき
    }

    public void ReStartGame() {
        pausePanel.SetActive(false);
        //reStartButton.SetActive(false);
        //pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
