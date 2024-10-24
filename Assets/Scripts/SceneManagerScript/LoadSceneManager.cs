using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// �V�[���J�ڂ��Ǘ�����X�N���v�g
/// Clear���\�b�h�͊e�X�e�[�W�̃N���A���ɌĂяo�����A
/// list.Clear()�Ɣ��̂Ŗ��O��ς���ׂ�
/// StageClear�Ƃ�
/// ���ƁAPlayerPrefs.SetInt�܂ł���Ă����̂��H
/// �P��ӔC�̌����ɔ����Ă���H
/// </summary>
public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private Fade fade;
    [SerializeField] private StageClearData stageClearData;

    private void Start()
    {
        Time.timeScale = 1f;
        Debug.Log("LoadSceneManager Start called");
        fade.FadeOut(1f);
    }
    public void Stage1() {
        fade.FadeIn(1f, () => {
            SceneManager.LoadScene("Stage1Scene");
        });
    }

    public void Stage2() {
        fade.FadeIn(1f, () => {
            SceneManager.LoadScene("Stage2Scene");
        });
    }

    public void Stage3() {
        fade.FadeIn(1f, () => {
            SceneManager.LoadScene("Stage3Scene");
        });
    }

    public void Select()
    {
        Debug.Log("Select method called");
        if(fade == null)
        {
            Debug.Log("fade is null");
        }
        Time.timeScale = 1f;
        fade.FadeIn(1f, () =>
        {
            Debug.Log("FadeIn callback called");
            SceneManager.LoadScene("SelectScene");
        });
    }

    public void Title()
    {
        fade.FadeIn(1f, () => {
            SceneManager.LoadScene("StartScene");
        });
    }

    public void Clear() {
        //�����P��ӔC�̌����ɔ����Ă���
        //�Q�[���̏�Ԃ�ۑ����郍�W�b�N��ʂ̃N���X�Ɉړ�
        //gameStateManager.SaveStageClearState(SceneManager.GetActiveScene().name);
        //PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);//1�͏����̏�

        Select();
    }

    public void Stage1Clear()
    {
        stageClearData.Stage1Cleared = true;
        Select();
    }

    public void Stage2Clear()
    {
        stageClearData.Stage2Cleared = true;
        Select();
    }

    public void Stage3Clear() {
        stageClearData.Stage3Cleared = true;
        Select();
    }


}
