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
public class SceneManagerScript : MonoBehaviour
{
    public void Stage1() {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void Stage2() {
        SceneManager.LoadScene("Stage2Scene");
    }

    public void Stage3() {
        SceneManager.LoadScene("Stage3Scene");
    }

    public void Select() {
        SceneManager.LoadScene("SelectScene");
    }

    public void Clear() {
        //�����P��ӔC�̌����ɔ����Ă���
        //�Q�[���̏�Ԃ�ۑ����郍�W�b�N��ʂ̃N���X�Ɉړ�
        //gameStateManager.SaveStageClearState(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);//1�͏����̏�
        Select();
    }

    public void Title() {
        SceneManager.LoadScene("StartScene");
    }
}
