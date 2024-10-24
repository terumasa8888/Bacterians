using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �X�e�[�W�Z���N�g��ʂ̃X�N���v�g
/// �����x�Ƃ����g���ăX�e�[�W�̉���󋵂�\�����Ă���
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject bgm;
    private AudioSource audioSource;

    [SerializeField] private GameObject stage2Button;
    [SerializeField] private GameObject stage3Button;

    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject panel3;

    [SerializeField] private GameObject imageLine2;
    [SerializeField] private GameObject imageLine3;

    private Image stage2ButtonImage;
    private Image stage3ButtonImage;

    [SerializeField] private GameObject AllClearUI;

    //[SerializeField] private Fade fade;
    [SerializeField] private StageClearData stageClearData;

    void Start()
    {
        audioSource = bgm.GetComponent<AudioSource>();

        stage2ButtonImage = stage2Button.GetComponent<Image>();
        stage3ButtonImage = stage3Button.GetComponent<Image>();

        stage2ButtonImage.color = TurnOffAlpha(stage2ButtonImage.color);
        stage3ButtonImage.color = TurnOffAlpha(stage3ButtonImage.color);

        InitializeStageButtons();
    }

    /// <summary>
    /// �e�X�e�[�W�̃{�^���̏�����
    /// </summary>
    void InitializeStageButtons()
    {
        if (stageClearData.Stage1Cleared)
        { // Stage1�N���A���Ă�Ȃ�
            stage2Button.SetActive(true);
            imageLine2.SetActive(true);
            stage2ButtonImage.color = TurnOnAlpha(stage2ButtonImage.color);
        }

        if (stageClearData.Stage2Cleared)
        { // Stage2�N���A���Ă�Ȃ�
            stage3Button.SetActive(true);
            imageLine3.SetActive(true);
            stage3ButtonImage.color = TurnOnAlpha(stage3ButtonImage.color);
        }

        if (stageClearData.Stage3Cleared && !stageClearData.AllCleared)
        { // Stage3�N���A���Ă��Ă����߂ẴN���A�Ȃ�
            AllClearUI.SetActive(true);
            stageClearData.AllCleared = true;
            audioSource.volume = 0.3f;
        }
    }

    /// <summary>
    /// �{�^���𓧖��ɂ���
    /// </summary>
    Color TurnOffAlpha(Color color)
    {
        color.a = 1.0f / 3.0f;
        return color;
    }

    /// <summary>
    /// �{�^����s�����ɂ���
    /// </summary>
    Color TurnOnAlpha(Color color)
    {
        color.a = 1;
        return color;
    }
}
