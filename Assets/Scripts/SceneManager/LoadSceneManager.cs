using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// シーン遷移を管理するスクリプト
/// </summary>
public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private Fade fade;
    [SerializeField] private StageClearData stageClearData;

    private void Start()
    {
        Time.timeScale = 1f;
        Debug.Log("LoadSceneManager Start called");
        fade.FadeOut(1f);
    }
    public void Stage1() {
        fade.FadeIn(1f, () => {
            SceneManager.LoadScene("Stage1Scene");
        });
    }

    public void Stage2() {
        fade.FadeIn(1f, () => {
            SceneManager.LoadScene("Stage2Scene");
        });
    }

    public void Stage3() {
        fade.FadeIn(1f, () => {
            SceneManager.LoadScene("Stage3Scene");
        });
    }

    public void Select()
    {
        Debug.Log("Select method called");
        if(fade == null)
        {
            Debug.Log("fade is null");
        }
        Time.timeScale = 1f;
        /*if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // WebGLでは直接シーンをロード
            SceneManager.LoadScene("SelectScene");
            return;
        }*/
        fade.FadeIn(1f, () =>
        {
            Debug.Log("FadeIn callback called");
            SceneManager.LoadScene("SelectScene");
        });
    }

    public void Title()
    {
        fade.FadeIn(1f, () => {
            SceneManager.LoadScene("StartScene");
        });
    }

    public void Stage1Clear()
    {
        stageClearData.Stage1Cleared = true;
        Select();
    }

    public void Stage2Clear()
    {
        stageClearData.Stage2Cleared = true;
        Select();
    }

    public void Stage3Clear() {
        stageClearData.Stage3Cleared = true;
        Select();
    }


}
