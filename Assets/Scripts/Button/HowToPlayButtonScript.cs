using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayManagerScript: MonoBehaviour
{
    public GameObject howToPlayUI1, howToPlayUI2, howToPlayUI3, backGround;

    public void HowToPlayButton() {
        backGround.SetActive(false);
        howToPlayUI1.SetActive(true);
    }

    public void Next() {
        howToPlayUI1.SetActive(false);
        howToPlayUI2.SetActive(true);
    }

    public void NextNext() {
        howToPlayUI2.SetActive(false);
        howToPlayUI3.SetActive(true);
    }

    public void End() {
        howToPlayUI3.SetActive(false);
        backGround.SetActive(true);
    }
}
