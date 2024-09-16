using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ԍo�߂ŐԂ��l�p�`�̃X�P�[�����g��k��������X�N���v�g
/// </summary>
/// <remarks>
/// �����o�ϐ�:
/// <para><b>float period:</b> �X�P�[���̊g��E�k���̎���</para>
/// <para><b>float time:</b> ���݂̎���</para>
/// <para><b>float changeSpeed:</b> �X�P�[���̕ω����x</para>
/// <para><b>float destroyTime:</b> �I�u�W�F�N�g���j�󂳂��܂ł̎���</para>
/// <para><b>bool enlarge:</b> �g�咆���ǂ����������t���O</para>
/// </remarks>
public class UIScaleScript : MonoBehaviour {
    public float period;
    public float time, changeSpeed, destroyTime;
    public bool enlarge;

    void Start() {
        //period = Mathf.PI;
        enlarge = true;
    }

    void Update() {
        changeSpeed = Time.deltaTime * 0.1f;
        destroyTime += Time.deltaTime;

        if (time < 0) {
            enlarge = true;
        }
        if (time > period) {
            enlarge = false;
        }

        if (enlarge == true) {
            time += Time.deltaTime;
            transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
        else {
            time -= Time.deltaTime;
            transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
        //15�b��Ɏ���������
        if(destroyTime > 15.0f) {
            Destroy(this.gameObject);
        }
    }
}
