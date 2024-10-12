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
        status.DuplicatableNumber
            .Where(number => number > 0)
            .ThrottleFirst(System.TimeSpan.FromSeconds(status.DuplicateInterval))
            .Subscribe(_ => Duplicate())
            .AddTo(this);
    }

    private void Duplicate()
    {
        GameObject clone = Instantiate(this.gameObject);
        clone.name = this.gameObject.name + "(Clone)";
        status.ReduceDuplicatableNumber();
    }
}
