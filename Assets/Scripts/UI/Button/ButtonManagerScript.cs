using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;


/// <summary>
/// 各キャラクターボタンや移動ボタン、サークルの表示、
/// ボタンサークルの回転を管理するスクリプト
/// 同じ処理なのでなんとかまとめられんかな
/// </summary>
public class ButtonManagerScript : MonoBehaviour {
    public GameObject saruButton;
    public GameObject houseDustButton;
    public GameObject clioneButton;
    public GameObject mijinkoButton;
    public GameObject piroriButton;
    public GameObject moveButton;

    public GameObject circle;
    public GameObject buttonCircle;

    public float angle = 1;
    public bool rot = true;

    private ReactiveProperty<ButtonType> selectedButtonType = new ReactiveProperty<ButtonType>(ButtonType.None);

    public IReadOnlyReactiveProperty<ButtonType> SelectedButtonType => selectedButtonType;

    void Start() {
        var buttons = new List<GameObject> { saruButton, houseDustButton, clioneButton, mijinkoButton, piroriButton, moveButton };

        foreach (var button in buttons) {
            var buttonScript = button.GetComponent<CharacterButtonScript>();
            buttonScript.OnClickAsObservable.Subscribe(clickedButton => OnButtonClicked(clickedButton));
        }
    }

    void LateUpdate() {
        if (rot) {
            buttonCircle.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
        }
    }

    private void OnButtonClicked(CharacterButtonScript clickedButton) {
        ResetOther();
        if (clickedButton.IsSelected()) {
            buttonCircle.GetComponent<RectTransform>().position = clickedButton.GetComponent<RectTransform>().position;
            buttonCircle.SetActive(true);
            selectedButtonType.Value = clickedButton.ButtonType; // 選択されたボタンの種類をReactivePropertyに保存
        }
        else {
            buttonCircle.SetActive(false);
            selectedButtonType.Value = ButtonType.None; // 選択が解除された場合は None にする
        }
    }

    public void ResetOther() {
        var buttons = new List<GameObject> { saruButton, houseDustButton, clioneButton, mijinkoButton, piroriButton, moveButton };
        foreach (var button in buttons) {
            button.GetComponent<CharacterButtonScript>().ResetSelection();
        }
        circle.SetActive(false);
        selectedButtonType.Value = ButtonType.None; // リセット時に選択を解除
    }
}
