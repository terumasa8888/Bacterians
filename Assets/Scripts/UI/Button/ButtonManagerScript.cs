using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;


/// <summary>
/// キャラクターボタンをまとめて管理
/// </summary>
public class ButtonManagerScript : MonoBehaviour {
    [SerializeField] private GameObject saruButton;
    [SerializeField] private GameObject houseDustButton;
    [SerializeField] private GameObject clioneButton;
    [SerializeField] private GameObject mijinkoButton;
    [SerializeField] private GameObject piroriButton;

    [SerializeField] private GameObject buttonCircle;

    private ReactiveProperty<ButtonType> selectedButtonType = new ReactiveProperty<ButtonType>(ButtonType.None);
    public IReadOnlyReactiveProperty<ButtonType> SelectedButtonType => selectedButtonType;

    private List<GameObject> buttons;

    void Start() {
        buttons = new List<GameObject>{
            saruButton,
            houseDustButton,
            clioneButton,
            mijinkoButton,
            piroriButton
        };
        foreach (var button in buttons) {
            CharacterButtonScript buttonScript = button.GetComponent<CharacterButtonScript>();
            buttonScript.OnClickAsObservable.Subscribe(clickedButton => OnButtonClicked(clickedButton));
        }
    }

    private void OnButtonClicked(CharacterButtonScript clickedButton)
    {
        // クリックされたボタンの状態を一時的に保存
        bool wasSelected = clickedButton.IsSelected();
        ResetOther();
        // クリックされたボタンの選択状態を切り替える
        clickedButton.SetSelected(wasSelected);

        if (!wasSelected)
        {
            Debug.Log("Button is not selected: " + clickedButton.ButtonType);
            buttonCircle.SetActive(false);
            selectedButtonType.Value = ButtonType.None;
        }
        else
        {
            Debug.Log("Button is selected: " + clickedButton.ButtonType);
            buttonCircle.GetComponent<RectTransform>().position = clickedButton.GetComponent<RectTransform>().position;
            buttonCircle.SetActive(true);
            selectedButtonType.Value = clickedButton.ButtonType;
        }
    }

    /// <summary>
    /// ボタンの選択状態をリセットする
    /// </summary>
    public void ResetOther() {
        foreach (var button in buttons) {
            button.GetComponent<CharacterButtonScript>().ResetSelection();
        }
        buttonCircle.SetActive(false);
        selectedButtonType.Value = ButtonType.None;
    }
}
