using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 2D オブジェクトのナビゲーションを制御するスクリプト
/// 基本的Enemyの追跡を制御する
/// </summary>
public class NavMeshAgent2D : MonoBehaviour
{
    [Header("Steering")]
    public float speed = 1.0f;
    public float stoppingDistance = 0;

    public Vector2 destination { get; set; }

    public bool SetDestination(Vector2 target)
    {
        destination = target;
        Trace(transform.position, target);
        return true;
    }

    private void Trace(Vector2 current, Vector2 target)
    {
        if (Vector2.Distance(current, target) <= stoppingDistance) return;

        NavMeshPath path = new NavMeshPath();
        if (!NavMesh.CalculatePath(current, target, NavMesh.AllAreas, path) || path.corners.Length == 0)
        {
            Debug.LogWarning("Path calculation failed or no corners found.");
            return;
        }

        Vector2 corner = path.corners[0];

        if (path.corners.Length > 1 && Vector2.Distance(current, corner) <= 0.05f)
        {
            corner = path.corners[1];
        }

        transform.position = Vector2.MoveTowards(current, corner, speed * Time.deltaTime);
    }
}
