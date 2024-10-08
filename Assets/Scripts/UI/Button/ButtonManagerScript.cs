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

        // �N���b�N���ꂽ�{�^���̏�Ԃ��ꎞ�I�ɕۑ�
        bool wasSelected = clickedButton.IsSelected();

        ResetOther();

        // �ꎞ�I�ɕۑ�������Ԃ��g�p���ď������s��
        if (wasSelected) {
            Debug.Log("Button is selected: " + clickedButton.ButtonType);
            buttonCircle.GetComponent<RectTransform>().position = clickedButton.GetComponent<RectTransform>().position;
            buttonCircle.SetActive(true);
            selectedButtonType.Value = clickedButton.ButtonType; // �I�����ꂽ�{�^���̎�ނ�ReactiveProperty�ɕۑ�
        }
        else {
            Debug.Log("Button is not selected: " + clickedButton.ButtonType);
            buttonCircle.SetActive(false);
            selectedButtonType.Value = ButtonType.None; // �I�����������ꂽ�ꍇ�� None �ɂ���
        }

        // �N���b�N���ꂽ�{�^���̑I����Ԃ��X�V
        clickedButton.ResetSelection();
        //clickedButton.OnButtonClicked();
    }

    public void ResetOther() {
        foreach (var button in buttons) {
            button.GetComponent<CharacterButtonScript>().ResetSelection();
        }
        //circle.SetActive(false);
        buttonCircle.SetActive(false);
        selectedButtonType.Value = ButtonType.None; // ���Z�b�g���ɑI��������
    }
}
