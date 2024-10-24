using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// UIを差し替えて遊び方や設定を表示するスクリプト
/// </summary>
public class StartSceneManager : MonoBehaviour
{

    [SerializeField] private GameObject[] howToPlayUIs;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject settingUI;

    private int currentIndex = 0;

    /// <summary>
    /// 遊び方を表示する、ボタンクリック時の処理
    /// </summary>
    public void ShowHowToPlay()
    {
        titleUI.SetActive(false);
        settingUI.SetActive(false);
        currentIndex = 0;
        UpdateHowToPlayUI();
    }

    /// <summary>
    /// 遊び方を非表示
    /// </summary>
    private void HideHowToPlay()
    {
        howToPlayUIs[currentIndex].SetActive(false);
        titleUI.SetActive(true);
    }

    /// <summary>
    /// 遊び方の画像を次に進める、クリック時の処理
    /// </summary>
    public void GetNextHowToPlayUI()
    {
        if (currentIndex >= howToPlayUIs.Length - 1)
        {
            HideHowToPlay();
            return;
        }

        currentIndex++;
        UpdateHowToPlayUI();
    }

    /// <summary>
    /// 遊び方の画像を更新
    /// </summary>
    private void UpdateHowToPlayUI()
    {
        for (int i = 0; i < howToPlayUIs.Length; i++)
        {
            howToPlayUIs[i].SetActive(i == currentIndex);
        }
    }

    /// <summary>
    /// 設定画面を表示する、ボタンクリック時の処理
    /// </summary>
    public void ShowSettingUI()
    {
        settingUI.SetActive(true);
    }

    /// <summary>
    /// 設定画面を非表示
    /// </summary>
    public void HideSettingUI()
    {
        settingUI.SetActive(false);
    }
}
