using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// リタイアボタンを押したときの処理
/// </summary>
public class RetireButtonScript : MonoBehaviour
{

    public void Retire() {
        SceneManager.LoadScene("SelectScene");
    }
}
