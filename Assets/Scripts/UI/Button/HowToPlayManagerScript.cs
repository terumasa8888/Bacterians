using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// 遊び方ボタンにアタッチするスクリプト
/// UIを差し替えて遊び方を表示する
/// </summary>
public class HowToPlayManagerScript : MonoBehaviour {
    [SerializeField]
    private GameObject[] howToPlayUIs;
    [SerializeField]
    private GameObject titleUI;

    private int currentIndex = 0;

    public void HowToPlayButton() {
        titleUI.SetActive(false);
        currentIndex = 0;
        UpdateUI();
    }

    public void Next() {
        if (currentIndex < howToPlayUIs.Length - 1) {
            currentIndex++;
            UpdateUI();
        }
        else {
            End();
        }
    }

    private void End() {
        howToPlayUIs[currentIndex].SetActive(false);
        titleUI.SetActive(true);
    }

    private void UpdateUI() {
        for (int i = 0; i < howToPlayUIs.Length; i++) {
            howToPlayUIs[i].SetActive(i == currentIndex);
        }
    }
}
