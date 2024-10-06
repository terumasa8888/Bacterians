using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// クリア音と花火を生成するスクリプト
/// 名前はFireWorksManagerだが、花火以外の処理も含まれている
/// なので、名前を変更するか、別のスクリプトに分ける
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
