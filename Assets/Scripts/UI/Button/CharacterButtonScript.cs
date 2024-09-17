using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各キャラクターボタンのスクリプト
/// CharacterButtonScriptという名前なのに
/// サークルの表示・位置変更までやってしまっている
/// OnClickは名前が紛らわしい
/// OnButtonClickedが存在するのでそっちに変更すべき？
/// しかも、名前が変わるたびにInspectorの設定も変更しないといけない
/// つまり、コードから自動的にOnButtonClickedを実行するほうがよい
/// (Inspectorから実行メソッドを設定しなくて済む)
/// IsClickedももっと良い名前に変更すべき
/// IsSelectedとか
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
        if (isClicked) {//すでにボタン押されているなら
            isClicked = false;
            //ボタンサークル(青)を非表示にする
            buttonManagerScript.buttonCircle.SetActive(false);
            //サークル(赤)を非表示にする
            buttonManagerScript.circle.SetActive(false);
        }
        else {//押されてないなら
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
    /// ボタンのクリック状態をリセットする
    /// </summary>
    public void ResetClickState() {
        isClicked = false;
    }


}
