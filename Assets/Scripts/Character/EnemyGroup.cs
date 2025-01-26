using UnityEngine;

// �t�F�[�Y�̒�`
// �t�F�[�Y�ɉ����đ��B�̊Ǘ����邽�߂�Player���g������
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


