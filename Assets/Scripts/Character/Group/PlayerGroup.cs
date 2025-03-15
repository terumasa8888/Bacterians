using System.Linq;
using UnityEngine;

public class PlayerGroup : Group
{
    public Transform GetCenterTransform()
    {
        // �v���C���[�O���[�v�̒��S�ɍł��߂��v���C���[���擾
        return Members.OrderBy(p => Vector3.Distance(p.position, GetCenter())).First();
    }
}
