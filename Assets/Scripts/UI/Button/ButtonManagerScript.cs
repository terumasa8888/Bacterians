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
    [SerializeField] private GameObject saruButton;
    [SerializeField] private GameObject houseDustButton;
    [SerializeField] private GameObject clioneButton;
    [SerializeField] private GameObject mijinkoButton;
    [SerializeField] private GameObject piroriButton;
    [SerializeField] private GameObject moveButton;

    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject buttonCircle;

    private float angle = 1;

    private ReactiveProperty<ButtonType> selectedButtonType = new ReactiveProperty<ButtonType>(ButtonType.None);

    public IReadOnlyReactiveProperty<ButtonType> SelectedButtonType => selectedButtonType;

    private List<GameObject> buttons;

    void Start() {
        buttons = new List<GameObject> { saruButton, houseDustButton, clioneButton, mijinkoButton, piroriButton, moveButton };

        foreach (var button in buttons) {
            var buttonScript = button.GetComponent<CharacterButtonScript>();
            buttonScript.OnClickAsObservable.Subscribe(clickedButton => OnButtonClicked(clickedButton));
        }
    }

    void LateUpdate() {
        buttonCircle.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
    }

    private void OnButtonClicked(CharacterButtonScript clickedButton) {
        Debug.Log("Button clicked: " + clickedButton.ButtonType);

        // クリックされたボタンの状態を一時的に保存
        bool wasSelected = clickedButton.IsSelected();

        ResetOther();

        // 一時的に保存した状態を使用して処理を行う
        if (wasSelected) {
            Debug.Log("Button is selected: " + clickedButton.ButtonType);
            buttonCircle.GetComponent<RectTransform>().position = clickedButton.GetComponent<RectTransform>().position;
            buttonCircle.SetActive(true);
            selectedButtonType.Value = clickedButton.ButtonType; // 選択されたボタンの種類をReactivePropertyに保存
        }
        else {
            Debug.Log("Button is not selected: " + clickedButton.ButtonType);
            buttonCircle.SetActive(false);
            selectedButtonType.Value = ButtonType.None; // 選択が解除された場合は None にする
        }

        // クリックされたボタンの選択状態を更新
        clickedButton.ResetSelection();
        //clickedButton.OnButtonClicked();
    }

    public void ResetOther() {
        foreach (var button in buttons) {
            button.GetComponent<CharacterButtonScript>().ResetSelection();
        }
        //circle.SetActive(false);
        buttonCircle.SetActive(false);
        selectedButtonType.Value = ButtonType.None; // リセット時に選択を解除
    }
}
