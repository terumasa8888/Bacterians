using UnityEngine;

/*public enum EnemyState
{
    Attack,
    CollectItem,
    Idle
}*/

public class EnemyGroup : Group
{
    public void SetState(CharacterState state, Transform target)
    {
        Debug.Log($"Group is now in {state} state targeting {target?.name}");
        foreach (var enemy in Members)
        {
            var behavior = enemy.GetComponent<EnemyBehavior>();
            if (behavior != null)
            {
                behavior.SetState(state, target);
            }
        }
    }
}


