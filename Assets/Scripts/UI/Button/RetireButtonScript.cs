using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���^�C�A�{�^�����������Ƃ��̏���
/// </summary>
public class RetireButtonScript : MonoBehaviour
{

    public void Retire() {
        SceneManager.LoadScene("SelectScene");
    }
}
