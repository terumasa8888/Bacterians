using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �N���A���Ɖԉ΂𐶐�����X�N���v�g
/// ���O��FireWorksManager�����A�ԉΈȊO�̏������܂܂�Ă���
/// �Ȃ̂ŁA���O��ύX���邩�A�ʂ̃X�N���v�g�ɕ�����
/// </summary>
public class FireWorksManager : MonoBehaviour
{
    public GameObject fireWorkPrefab;
    public GameObject clearSound;
    float timer;

    void Start()
    {
        Instantiate(clearSound);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.5) {
                float x = Random.Range(-10, 10);
                float y = Random.Range(-5, 5);
                Vector3 v = new Vector3(x, y, -8);
                timer = 0;
                Instantiate(fireWorkPrefab, v, Quaternion.identity);
        }
    }
}
