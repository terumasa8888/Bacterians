using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// UI�������ւ��ėV�ѕ���ݒ��\������X�N���v�g
/// </summary>
public class StartSceneManager : MonoBehaviour
{

    [SerializeField] private GameObject[] howToPlayUIs;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject settingUI;

    private int currentIndex = 0;

    /// <summary>
    /// �V�ѕ���\������A�{�^���N���b�N���̏���
    /// </summary>
    public void ShowHowToPlay()
    {
        titleUI.SetActive(false);
        settingUI.SetActive(false);
        currentIndex = 0;
        UpdateHowToPlayUI();
    }

    /// <summary>
    /// �V�ѕ����\��
    /// </summary>
    private void HideHowToPlay()
    {
        howToPlayUIs[currentIndex].SetActive(false);
        titleUI.SetActive(true);
    }

    /// <summary>
    /// �V�ѕ��̉摜�����ɐi�߂�A�N���b�N���̏���
    /// </summary>
    public void GetNextHowToPlayUI()
    {
        if (currentIndex >= howToPlayUIs.Length - 1)
        {
            HideHowToPlay();
            return;
        }

        currentIndex++;
        UpdateHowToPlayUI();
    }

    /// <summary>
    /// �V�ѕ��̉摜���X�V
    /// </summary>
    private void UpdateHowToPlayUI()
    {
        for (int i = 0; i < howToPlayUIs.Length; i++)
        {
            howToPlayUIs[i].SetActive(i == currentIndex);
        }
    }

    /// <summary>
    /// �ݒ��ʂ�\������A�{�^���N���b�N���̏���
    /// </summary>
    public void ShowSettingUI()
    {
        settingUI.SetActive(true);
    }

    /// <summary>
    /// �ݒ��ʂ��\��
    /// </summary>
    public void HideSettingUI()
    {
        settingUI.SetActive(false);
    }
}
