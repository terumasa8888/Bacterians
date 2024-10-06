using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;


/// <summary>
/// �e�L�����N�^�[�{�^����ړ��{�^���A�T�[�N���̕\���A
/// �{�^���T�[�N���̉�]���Ǘ�����X�N���v�g
/// ���������Ȃ̂łȂ�Ƃ��܂Ƃ߂��񂩂�
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
            selectedButtonType.Value = clickedButton.ButtonType; // �I�����ꂽ�{�^���̎�ނ�ReactiveProperty�ɕۑ�
        }
        else {
            buttonCircle.SetActive(false);
            selectedButtonType.Value = ButtonType.None; // �I�����������ꂽ�ꍇ�� None �ɂ���
        }
    }

    public void ResetOther() {
        var buttons = new List<GameObject> { saruButton, houseDustButton, clioneButton, mijinkoButton, piroriButton, moveButton };
        foreach (var button in buttons) {
            button.GetComponent<CharacterButtonScript>().ResetSelection();
        }
        circle.SetActive(false);
        selectedButtonType.Value = ButtonType.None; // ���Z�b�g���ɑI��������
    }
}
