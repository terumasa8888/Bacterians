using UnityEngine;

// フェーズの定義
// フェーズに応じて増殖の管理するためにPlayerも使うかも
public enum EnemyPhase
{
    Attack,
    CollectItem,
    Wait
}

public class EnemyGroup : Group
{
    public void SetPhase(EnemyPhase phase, Transform target)
    {
        Debug.Log($"Group is now in {phase} phase targeting {target?.name}");
        foreach (var enemy in Members)
        {
            var behavior = enemy.GetComponent<EnemyBehavior>();
            if (behavior != null)
            {
                behavior.SetPhase(phase, target);
            }
        }
    }
}


