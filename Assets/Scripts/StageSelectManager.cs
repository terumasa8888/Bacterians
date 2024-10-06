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
    bool isCalledOnce = false;
    //bool isCalledOnceAfterAllClear = false;

    public GameObject bgm;
    AudioSource audioSource;

    public GameObject stage2Button;
    public GameObject stage3Button;

    public GameObject panel2;
    public GameObject panel3;

    public GameObject imageLine2;
    public GameObject imageLine3;

    Image stage2ButtonImage;
    Image stage3ButtonImage;

    public GameObject AllClearUI;


    void Start()
    {
        audioSource = bgm.GetComponent<AudioSource>();

        //PlayerPrefs.SetInt("AllClear", 0);//実験用
        stage2ButtonImage = stage2Button.GetComponent<Image>();
        stage3ButtonImage = stage3Button.GetComponent<Image>();

        stage2ButtonImage.color = TurnOffAlpha(stage2ButtonImage.color);
        stage3ButtonImage.color = TurnOffAlpha(stage3ButtonImage.color);
    }

    void Update()
    {
        if (!isCalledOnce) {
            isCalledOnce = true;
            if (PlayerPrefs.GetInt("Stage1Scene", 0) == 1) {//Stage1クリアしてるなら
                //panel2.SetActive(false);
                stage2Button.SetActive(true);//なんかうまくいかんので追加
                imageLine2.SetActive(true);
                stage2ButtonImage.color = TurnOnAlpha(stage2ButtonImage.color);
            }

            if (PlayerPrefs.GetInt("Stage2Scene", 0) == 1) {//Stage2クリアしてるなら
                //panel3.SetActive(false);
                stage3Button.SetActive(true);//なんかうまくいかんので追加
                imageLine3.SetActive(true);
                stage3ButtonImage.color = TurnOnAlpha(stage3ButtonImage.color);
            }

            if ((PlayerPrefs.GetInt("Stage3Scene", 0) == 1) && (PlayerPrefs.GetInt("AllClear") == 0)) {//Stage3クリアしていてかつ初めてのクリアなら
                AllClearUI.SetActive(true);
                PlayerPrefs.SetInt("AllClear", 1);
                audioSource.volume = 0.3f;

            }
        }
    }

    Color TurnOffAlpha(Color color) {
        color.a = 1.0f / 3.0f;
        return color;
    }

    Color TurnOnAlpha(Color color) {
        color.a = 1;
        return color;
    }
}
