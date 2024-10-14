using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �e�L�����I���{�^�����Ǘ�
/// </summary>
public class CharacterButtonScript : MonoBehaviour {
    private bool isSelected = false;
    private Subject<CharacterButtonScript> onClickSubject = new Subject<CharacterButtonScript>();

    public IObservable<CharacterButtonScript> OnClickAsObservable => onClickSubject;

    [SerializeField]
    private ButtonType buttonType;

    public ButtonType ButtonType {
        get => buttonType;
    }

    void Start() {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    public void OnButtonClicked() {
        isSelected = !isSelected;
        onClickSubject.OnNext(this);
    }

    public bool IsSelected() {
        return isSelected;
    }

    public void ResetSelection() {
        isSelected = false;
    }

    public void SetSelected(bool selected){
        isSelected = selected;
    }

}
