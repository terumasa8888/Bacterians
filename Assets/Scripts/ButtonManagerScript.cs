using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManagerScript : MonoBehaviour
{
    //ButtonêÈåæ
    public GameObject saruButton;
    public GameObject houseDustButton;
    public GameObject clioneButton;
    public GameObject mijinkoButton;
    public GameObject piroriButton;
    public GameObject moveButton;
    public GameObject circle;
    public GameObject buttonCircle;


    //ButtonScriptêÈåæ
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
        saruButtonScript.tf = false;
        houseDustButtonScript.tf = false;
        clioneButtonScript.tf = false;
        mijinkoButtonScript.tf = false;
        piroriButtonScript.tf = false;
        moveButtonScript.tf = false;
        circle.SetActive(false);
    }

    public int GetPlayerNumber() {
        if (saruButtonScript.tf) {
            return 1;
        }
        else if (houseDustButtonScript.tf) {
            return 2;
        }
        else if (clioneButtonScript.tf) {
            return 3;
        }
        else if (mijinkoButtonScript.tf) {
            return 4;
        }
        else if (piroriButtonScript.tf) {
            return 5;
        }
        else if (moveButtonScript.tf) {
            return 6;
        }
        else {
            return 0;
        }
    }
}
