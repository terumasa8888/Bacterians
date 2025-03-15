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
    [SerializeField] private GameObject bgmManager;
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

    private const float DisabledTransparency = 1.0f / 3.0f;
    private const float EnabledTransparency = 1.0f;

    void Start()
    {
        audioSource = bgmManager.GetComponent<AudioSource>();

        stage2ButtonImage = stage2Button.GetComponent<Image>();
        stage3ButtonImage = stage3Button.GetComponent<Image>();

        SetButtonTransparency(stage2ButtonImage, DisabledTransparency);
        SetButtonTransparency(stage3ButtonImage, DisabledTransparency);

        Debug.Log(stageClearData.AllCleared);
        InitializeStageButtons();
    }

    /// <summary>
    /// 各ステージのボタンの初期化
    /// </summary>
    void InitializeStageButtons()
    {
        if (stageClearData.Stage1Cleared)
        { // Stage1クリアしてるなら
            panel2.SetActive(false);
            imageLine2.SetActive(true);
            SetButtonTransparency(stage2ButtonImage, EnabledTransparency);
        }

        if (stageClearData.Stage2Cleared)
        { // Stage2クリアしてるなら
            panel3.SetActive(false);
            imageLine3.SetActive(true);
            SetButtonTransparency(stage3ButtonImage, EnabledTransparency);
        }

        if (stageClearData.Stage3Cleared && !stageClearData.AllCleared)
        { // Stage3クリアしていてかつ初めてのクリアなら
            AllClearUI.SetActive(true);
            stageClearData.AllCleared = true;
            audioSource.volume = 0.3f; // BGMの音量を直接設定
        }
    }

    /// <summary>
    /// ボタンの透明度を設定する
    /// </summary>
    void SetButtonTransparency(Image buttonImage, float transparency)
    {
        Color color = buttonImage.color;
        color.a = transparency;
        buttonImage.color = color;
    }
}
