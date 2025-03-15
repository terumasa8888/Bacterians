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
    [SerializeField] private GameObject bgmManager;
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

    private const float DisabledTransparency = 1.0f / 3.0f;
    private const float EnabledTransparency = 1.0f;

    void Start()
    {
        audioSource = bgmManager.GetComponent<AudioSource>();

        stage2ButtonImage = stage2Button.GetComponent<Image>();
        stage3ButtonImage = stage3Button.GetComponent<Image>();

        SetButtonTransparency(stage2ButtonImage, DisabledTransparency);
        SetButtonTransparency(stage3ButtonImage, DisabledTransparency);

        Debug.Log(stageClearData.AllCleared);
        InitializeStageButtons();
    }

    /// <summary>
    /// �e�X�e�[�W�̃{�^���̏�����
    /// </summary>
    void InitializeStageButtons()
    {
        if (stageClearData.Stage1Cleared)
        { // Stage1�N���A���Ă�Ȃ�
            panel2.SetActive(false);
            imageLine2.SetActive(true);
            SetButtonTransparency(stage2ButtonImage, EnabledTransparency);
        }

        if (stageClearData.Stage2Cleared)
        { // Stage2�N���A���Ă�Ȃ�
            panel3.SetActive(false);
            imageLine3.SetActive(true);
            SetButtonTransparency(stage3ButtonImage, EnabledTransparency);
        }

        if (stageClearData.Stage3Cleared && !stageClearData.AllCleared)
        { // Stage3�N���A���Ă��Ă����߂ẴN���A�Ȃ�
            AllClearUI.SetActive(true);
            stageClearData.AllCleared = true;
            audioSource.volume = 0.3f; // BGM�̉��ʂ𒼐ڐݒ�
        }
    }

    /// <summary>
    /// �{�^���̓����x��ݒ肷��
    /// </summary>
    void SetButtonTransparency(Image buttonImage, float transparency)
    {
        Color color = buttonImage.color;
        color.a = transparency;
        buttonImage.color = color;
    }
}
