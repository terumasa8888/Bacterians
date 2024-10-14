using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 増殖する機能を提供するクラス
/// </summary>
public class Duplicatable : MonoBehaviour
{
    private Status status;

    void Start()
    {
        status = GetComponent<Status>();

        // Observable.Intervalを使用して増殖のタイミングを制御
        Observable.Interval(System.TimeSpan.FromSeconds(status.DuplicateInterval))
            .Where(_ => status.DuplicatableNumber.Value > 0)
            .Subscribe(_ => Duplicate())
            .AddTo(this);
    }

    private void Duplicate()
    {
        GameObject clone = Instantiate(this.gameObject);
        //clone.name = this.gameObject.name + "(Clone)";

        // クローンのStatusコンポーネントを取得し、DuplicatableNumberを設定
        Status cloneStatus = clone.GetComponent<Status>();
        cloneStatus.SetDuplicatableNumber(status.DuplicatableNumber.Value - 1);

        status.ReduceDuplicatableNumber();
    }
}
