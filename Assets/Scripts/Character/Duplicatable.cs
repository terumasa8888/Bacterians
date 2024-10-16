using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// ���B����@�\��񋟂���N���X
/// </summary>
public class Duplicatable : MonoBehaviour
{
    private Status status;

    void Start()
    {
        status = GetComponent<Status>();

        Observable.Interval(System.TimeSpan.FromSeconds(status.DuplicateInterval))
            .Where(_ => status.DuplicatableNumber.Value > 0)
            .Subscribe(_ => Duplicate())
            .AddTo(this);
    }

    private void Duplicate()
    {
        GameObject clone = Instantiate(this.gameObject);
        Status cloneStatus = clone.GetComponent<Status>();
        cloneStatus.SetDuplicatableNumber(status.DuplicatableNumber.Value - 1);//����ReduceDuplicatableNumber�ɕύX�\�H

        status.ReduceDuplicatableNumber();
    }
}
