using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgent2D : MonoBehaviour
{
    [Header("Steering")]
    public float speed = 1.0f;
    public float stoppingDistance = 0;

    [HideInInspector] // 常にUnityエディタから非表示
    private Vector2 trace_area = Vector2.zero;
    private float originalSpeed;

    // 新しいプロパティの追加
    public int avoidancePriority { get; private set; }

    private void Awake()
    {
        // avoidancePriorityをランダムに設定
        avoidancePriority = Random.Range(0, 100);
    }

    public Vector2 destination
    {
        get { return trace_area; }
        set
        {
            trace_area = value;
            Trace(transform.position, value);
        }
    }

    public bool SetDestination(Vector2 target)
    {
        destination = target;
        return true;
    }

    public void Stop()
    {
        originalSpeed = speed;
        speed = 0;
    }

    public void Resume()
    {
        speed = originalSpeed;
        //Debug.Log("Resume speed: " + speed);
    }

    private void Trace(Vector2 current, Vector2 target)
    {
        if (Vector2.Distance(current, target) <= stoppingDistance)
        {
            return;
        }

        // NavMesh に応じて経路を求める
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(current, target, NavMesh.AllAreas, path) && path.corners.Length > 0)
        {
            Vector2 corner = path.corners[0];

            if (path.corners.Length > 1 && Vector2.Distance(current, corner) <= 0.05f)
            {
                corner = path.corners[1];
            }

            // 衝突回避のロジックを追加
            AvoidCollisions(ref corner);

            transform.position = Vector2.MoveTowards(current, corner, speed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Path calculation failed or no corners found.");
        }
    }

    private void AvoidCollisions(ref Vector2 corner)
    {
        // 他のNavMeshAgent2Dを取得
        NavMeshAgent2D[] agents = FindObjectsOfType<NavMeshAgent2D>();
        foreach (NavMeshAgent2D agent in agents)
        {
            if (agent == this) continue;

            // 距離が近い場合、優先順位に基づいて回避
            if (Vector2.Distance(transform.position, agent.transform.position) < 1.0f)
            {
                if (avoidancePriority < agent.avoidancePriority)
                {
                    // 自分の優先順位が低い場合、回避する
                    Vector2 direction = (transform.position - agent.transform.position).normalized;
                    corner += direction * 0.5f;
                }
            }
        }
    }
}
