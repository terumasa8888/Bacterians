using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// シーン遷移を管理するスクリプト
/// Clearメソッドは各ステージのクリア時に呼び出すが、
/// list.Clear()と被るので名前を変えるべき
/// StageClearとか
/// あと、PlayerPrefs.SetIntまでやっていいのか？
/// 単一責任の原則に反している？
/// </summary>
public class SceneManagerScript : MonoBehaviour
{
    public void Stage1() {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void Stage2() {
        SceneManager.LoadScene("Stage2Scene");
    }

    public void Stage3() {
        SceneManager.LoadScene("Stage3Scene");
    }

    public void Select() {
        SceneManager.LoadScene("SelectScene");
    }

    public void Clear() {
        //ここ単一責任の原則に反している
        //ゲームの状態を保存するロジックを別のクラスに移動
        //gameStateManager.SaveStageClearState(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);//1は勝利の証
        Select();
    }

    public void Title() {
        SceneManager.LoadScene("StartScene");
    }
}
