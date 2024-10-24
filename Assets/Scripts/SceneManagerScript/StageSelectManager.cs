using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ステージセレクト画面のスクリプト
/// 透明度とかを使ってステージの解放状況を表現している
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject bgm;
    private AudioSource audioSource;

    [SerializeField] private GameObject stage2Button;
    [SerializeField] private GameObject stage3Button;

    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject panel3;

    [SerializeField] private GameObject imageLine2;
    [SerializeField] private GameObject imageLine3;

    private Image stage2ButtonImage;
    private Image stage3ButtonImage;

    [SerializeField] private GameObject AllClearUI;

    //[SerializeField] private Fade fade;
    [SerializeField] private StageClearData stageClearData;

    void Start()
    {
        audioSource = bgm.GetComponent<AudioSource>();

        stage2ButtonImage = stage2Button.GetComponent<Image>();
        stage3ButtonImage = stage3Button.GetComponent<Image>();

        stage2ButtonImage.color = TurnOffAlpha(stage2ButtonImage.color);
        stage3ButtonImage.color = TurnOffAlpha(stage3ButtonImage.color);

        InitializeStageButtons();
    }

    /// <summary>
    /// 各ステージのボタンの初期化
    /// </summary>
    void InitializeStageButtons()
    {
        if (stageClearData.Stage1Cleared)
        { // Stage1クリアしてるなら
            stage2Button.SetActive(true);
            imageLine2.SetActive(true);
            stage2ButtonImage.color = TurnOnAlpha(stage2ButtonImage.color);
        }

        if (stageClearData.Stage2Cleared)
        { // Stage2クリアしてるなら
            stage3Button.SetActive(true);
            imageLine3.SetActive(true);
            stage3ButtonImage.color = TurnOnAlpha(stage3ButtonImage.color);
        }

        if (stageClearData.Stage3Cleared && !stageClearData.AllCleared)
        { // Stage3クリアしていてかつ初めてのクリアなら
            AllClearUI.SetActive(true);
            stageClearData.AllCleared = true;
            audioSource.volume = 0.3f;
        }
    }

    /// <summary>
    /// ボタンを透明にする
    /// </summary>
    Color TurnOffAlpha(Color color)
    {
        color.a = 1.0f / 3.0f;
        return color;
    }

    /// <summary>
    /// ボタンを不透明にする
    /// </summary>
    Color TurnOnAlpha(Color color)
    {
        color.a = 1;
        return color;
    }
}
