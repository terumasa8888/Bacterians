using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    private List<Transform> playerTransforms;
    private List<Transform> enemyTransforms;
    private List<Transform> itemTransforms;

    private List<EnemyGroup> enemyGroups;
    private List<PlayerGroup> playerGroups;

    [SerializeField] private int enemyClusterCount = 3; // グループ数
    [SerializeField] private float updateInterval = 5f; // 更新間隔

    private float timer;

    void Start()
    {
        timer = updateInterval;
        enemyGroups = new List<EnemyGroup>();
        playerGroups = new List<PlayerGroup>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            UpdateGroupsAndStates();
            timer = updateInterval;
        }
    }

    /// <summary>
    /// グループと状態を更新
    /// </summary>
    void UpdateGroupsAndStates()
    {
        playerTransforms = GameObject.FindGameObjectsWithTag("Player").Select(go => go.transform).ToList();
        enemyTransforms = GameObject.FindGameObjectsWithTag("Enemy").Select(go => go.transform).ToList();
        itemTransforms = GameObject.FindGameObjectsWithTag("Item").Select(go => go.transform).ToList();

        // プレイヤーと敵をそれぞれクラスタリング
        playerGroups = ClusterGroups<PlayerGroup>(playerTransforms, enemyClusterCount);
        enemyGroups = ClusterGroups<EnemyGroup>(enemyTransforms, enemyClusterCount);

        // 各エネミーグループの、プレイヤーグループに対する最短距離を計算
        var distances = new List<(EnemyGroup group, float distance)>();
        foreach (var enemyGroup in enemyGroups)
        {
            float minDistance = float.MaxValue;
            foreach (var playerGroup in playerGroups)
            {
                float distance = Vector3.Distance(enemyGroup.GetCenter(), playerGroup.GetCenter());
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            distances.Add((enemyGroup, minDistance));
        }

        // 距離を昇順でソート
        distances = distances.OrderBy(d => d.distance).ToList();

        // フェーズを割り当て
        for (int i = 0; i < distances.Count; i++)
        {
            var (group, _) = distances[i];
            if (i == 0)
            {
                // 最短距離のグループはAttackフェーズ
                group.SetState(CharacterState.Attack, GetClosestPlayerGroup(group));
            }
            else if (i == 1)
            {
                // 2番目に最短距離のグループはCollectItemフェーズ
                group.SetState(CharacterState.CollectItem, GetClosestItem(group));
            }
            else
            {
                // 残りのグループはIdleフェーズ
                group.SetState(CharacterState.Idle, null);
            }
        }
    }

    /// <summary>
    /// オブジェクトたちをクラスタリングしてグループを作成
    /// </summary>
    List<TGroup> ClusterGroups<TGroup>(List<Transform> objects, int clusterCount) where TGroup : Group, new()
    {
        return ClusterObjects(objects, clusterCount)
            .Select(cluster => new TGroup { Members = cluster.Value }).ToList();
    }


    /// <summary>
    /// オブジェクトたちをクラスタリング
    /// </summary>
    Dictionary<int, List<Transform>> ClusterObjects(List<Transform> objects, int clusterCount)
    {
        Dictionary<int, List<Transform>> clusters = new Dictionary<int, List<Transform>>();

        if (objects.Count == 0)
        {
            return clusters;
        }

        // 初期のセントロイドをランダムに選択
        List<Vector3> centroids = new List<Vector3>();
        for (int i = 0; i < clusterCount; i++)
        {
            // objectsの中から、ランダムに選択したものをセントロイドとして追加
            centroids.Add(objects[Random.Range(0, objects.Count)].position);
        }

        bool hasChanged;

        do
        {
            // クラスタをクリア
            clusters.Clear();
            for (int i = 0; i < clusterCount; i++)
            {
                clusters[i] = new List<Transform>();
            }

            // 各オブジェクトを最も近いセントロイドのクラスタに割り当て
            foreach (var obj in objects)
            {
                int closestCluster = 0;
                float minDistance = Vector3.Distance(obj.position, centroids[0]);

                for (int i = 1; i < clusterCount; i++)
                {
                    float distance = Vector3.Distance(obj.position, centroids[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestCluster = i;
                    }
                }

                clusters[closestCluster].Add(obj);
            }

            hasChanged = false;
            // 各クラスタの新しいセントロイドを計算
            for (int i = 0; i < clusterCount; i++)
            {
                if (clusters[i].Count > 0)
                {
                    Vector3 newCentroid = ComputeCentroid(clusters[i]);
                    if (newCentroid != centroids[i])
                    {
                        centroids[i] = newCentroid;
                        hasChanged = true;
                    }
                }
            }
        } while (hasChanged); // セントロイドが変わらなくなるまで繰り返す

        return clusters;
    }

    Vector3 ComputeCentroid(List<Transform> points)
    {
        // クラスタ内のオブジェクトの中心を計算
        Vector3 centroid = Vector3.zero;
        foreach (var point in points)
        {
            centroid += point.position;
        }
        return centroid / points.Count;
    }

    /// <summary>
    /// これ似たような処理がさっきあったから、それ使えないかな
    /// </summary>
    /// <param name="enemyGroup"></param>
    /// <returns></returns>
    Transform GetClosestPlayerGroup(EnemyGroup enemyGroup)
    {
        // 最も近いプレイヤーグループを取得
        Transform closestGroup = null;
        float minDistance = float.MaxValue;

        foreach (var playerGroup in playerGroups)
        {
            float distance = Vector3.Distance(enemyGroup.GetCenter(), playerGroup.GetCenter());
            if (distance < minDistance)
            {
                minDistance = distance;
                closestGroup = playerGroup.GetCenterTransform();
            }
        }

        return closestGroup;
    }

    /// <summary>
    /// 最も近いアイテムを取得
    /// </summary>
    Transform GetClosestItem(EnemyGroup group)
    {
        Transform closestItem = null;
        float minDistance = float.MaxValue;

        foreach (var item in itemTransforms)
        {
            float distance = Vector3.Distance(group.GetCenter(), item.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestItem = item;
            }
        }

        return closestItem;
    }
}
