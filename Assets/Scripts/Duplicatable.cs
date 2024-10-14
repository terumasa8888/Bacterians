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

        // Observable.Interval���g�p���đ��B�̃^�C�~���O�𐧌�
        Observable.Interval(System.TimeSpan.FromSeconds(status.DuplicateInterval))
            .Where(_ => status.DuplicatableNumber.Value > 0)
            .Subscribe(_ => Duplicate())
            .AddTo(this);
    }

    private void Duplicate()
    {
        GameObject clone = Instantiate(this.gameObject);
        //clone.name = this.gameObject.name + "(Clone)";

        // �N���[����Status�R���|�[�l���g���擾���ADuplicatableNumber��ݒ�
        Status cloneStatus = clone.GetComponent<Status>();
        cloneStatus.SetDuplicatableNumber(status.DuplicatableNumber.Value - 1);

        status.ReduceDuplicatableNumber();
    }
}
