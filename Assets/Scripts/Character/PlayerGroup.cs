using System.Linq;
using UnityEngine;

public class PlayerGroup : Group
{
    public Transform GetCenterTransform()
    {
        // プレイヤーグループの中心に最も近いプレイヤーを取得
        return Members.OrderBy(p => Vector3.Distance(p.position, GetCenter())).First();
    }
}
