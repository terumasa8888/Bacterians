using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonScript : MonoBehaviour
{
    public bool tf = false;

    private GameObject buttonManager;
    ButtonManagerScript buttonManagerScript;

    void Start()
    {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();
    }

    public void Click() {
        if (tf) {//すでにボタン押されているなら
            tf = false;
            buttonManagerScript.buttonCircle.SetActive(false);
            buttonManagerScript.circle.SetActive(false);
        }
        else {//押されてないなら
            buttonManagerScript.ResetOther();
            tf = true;
            buttonManagerScript.buttonCircle.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            buttonManagerScript.buttonCircle.SetActive(true);
        }
    }
}
