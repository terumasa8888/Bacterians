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

            transform.position = Vector2.MoveTowards(current, corner, speed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Path calculation failed or no corners found.");
        }
    }
}
