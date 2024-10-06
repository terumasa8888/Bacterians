using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 時間経過で赤い四角形のスケールを拡大縮小させるスクリプト
/// </summary>
/// <remarks>
/// メンバ変数:
/// <para><b>float period:</b> スケールの拡大・縮小の周期</para>
/// <para><b>float time:</b> 現在の時間</para>
/// <para><b>float changeSpeed:</b> スケールの変化速度</para>
/// <para><b>float destroyTime:</b> オブジェクトが破壊されるまでの時間</para>
/// <para><b>bool enlarge:</b> 拡大中かどうかを示すフラグ</para>
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
        //15秒後に自分を消す
        if(destroyTime > 15.0f) {
            Destroy(this.gameObject);
        }
    }
}
