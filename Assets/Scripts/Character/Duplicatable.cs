using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 増殖する機能を提供するクラス
/// これは共通機能なので、CharacterBaseに実装するべき
/// </summary>
public class Duplicatable : MonoBehaviour
{
    private IStatus status;

    void Start()
    {
        status = GetComponent<IStatus>();

        Observable.Interval(System.TimeSpan.FromSeconds(status.DuplicateInterval))
            .Where(_ => status.DuplicatableNumber.Value > 0)
            .Subscribe(_ => Duplicate())
            .AddTo(this);
    }

    private void Duplicate()
    {
        GameObject clone = Instantiate(this.gameObject);
        IStatus cloneStatus = clone.GetComponent<IStatus>();
        cloneStatus.SetDuplicatableNumber(status.DuplicatableNumber.Value - 1);//これ外部からsetしてるの良くないかも

        status.ReduceDuplicatableNumber();
    }
}
