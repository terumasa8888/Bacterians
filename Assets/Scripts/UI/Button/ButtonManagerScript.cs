using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// �e�L�����N�^�[�{�^����ړ��{�^���A�T�[�N���̕\���A
/// �{�^���T�[�N���̉�]���Ǘ�����X�N���v�g
/// ���������Ȃ̂łȂ�Ƃ��܂Ƃ߂��񂩂�
/// </summary>
public class ButtonManagerScript : MonoBehaviour
{
    //Button�錾
    public GameObject saruButton;
    public GameObject houseDustButton;
    public GameObject clioneButton;
    public GameObject mijinkoButton;
    public GameObject piroriButton;
    public GameObject moveButton;

    public GameObject circle;
    public GameObject buttonCircle;


    //ButtonScript�錾
    CharacterButtonScript saruButtonScript;
    CharacterButtonScript houseDustButtonScript;
    CharacterButtonScript clioneButtonScript;
    CharacterButtonScript mijinkoButtonScript;
    CharacterButtonScript piroriButtonScript;
    CharacterButtonScript moveButtonScript;

    public float angle = 1;
    public bool rot = true;



    void Start()
    {

        saruButtonScript = saruButton.GetComponent<CharacterButtonScript>();
        houseDustButtonScript = houseDustButton.GetComponent<CharacterButtonScript>();
        clioneButtonScript = clioneButton.GetComponent<CharacterButtonScript>();
        mijinkoButtonScript = mijinkoButton.GetComponent<CharacterButtonScript>();
        piroriButtonScript = piroriButton.GetComponent<CharacterButtonScript>();
        moveButtonScript = moveButton.GetComponent<CharacterButtonScript>();

    }

    void LateUpdate() {
        if (rot) {
            buttonCircle.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
        }
    }


    public void ResetOther() {
        saruButtonScript.ResetClickState();
        houseDustButtonScript.ResetClickState();
        clioneButtonScript.ResetClickState();
        mijinkoButtonScript.ResetClickState();
        piroriButtonScript.ResetClickState();
        moveButtonScript.ResetClickState();
        circle.SetActive(false);
    }

    public int GetPlayerNumber() {
        if (saruButtonScript.IsClicked()) {
            return 1;
        }
        else if (houseDustButtonScript.IsClicked()) {
            return 2;
        }
        else if (clioneButtonScript.IsClicked()) {
            return 3;
        }
        else if (mijinkoButtonScript.IsClicked()) {
            return 4;
        }
        else if (piroriButtonScript.IsClicked()) {
            return 5;
        }
        else if (moveButtonScript.IsClicked()) {
            return 6;
        }
        else {
            return 0;
        }
    }
}
