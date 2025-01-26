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

    [SerializeField] private int enemyClusterCount = 3; // �O���[�v��
    [SerializeField] private float updateInterval = 5f; // �X�V�Ԋu

    private float timer;

    void Start()
    {
        // �^�C�}�[��������
        timer = updateInterval;
        // �O���[�v���X�g��������
        enemyGroups = new List<EnemyGroup>();
        playerGroups = new List<PlayerGroup>();
    }

    void Update()
    {
        // �^�C�}�[������
        timer -= Time.deltaTime;
        // �^�C�}�[��0�ȉ��ɂȂ�����O���[�v�ƃt�F�[�Y���X�V
        if (timer <= 0f)
        {
            UpdateGroupsAndPhases();
            // �^�C�}�[�����Z�b�g
            timer = updateInterval;
        }
    }

    void UpdateGroupsAndPhases()
    {
        // ���I�ɃI�u�W�F�N�g���擾
        playerTransforms = GameObject.FindGameObjectsWithTag("Player").Select(go => go.transform).ToList();
        enemyTransforms = GameObject.FindGameObjectsWithTag("Enemy").Select(go => go.transform).ToList();
        itemTransforms = GameObject.FindGameObjectsWithTag("Item").Select(go => go.transform).ToList();

        // �v���C���[�ƓG�����ꂼ��N���X�^�����O
        playerGroups = ClusterGroups<PlayerGroup>(playerTransforms, enemyClusterCount);
        enemyGroups = ClusterGroups<EnemyGroup>(enemyTransforms, enemyClusterCount);

        // �e�G�l�~�[�O���[�v�̍ŒZ�������v�Z
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

        // �����Ń\�[�g
        distances = distances.OrderBy(d => d.distance).ToList();

        // �t�F�[�Y�����蓖��
        for (int i = 0; i < distances.Count; i++)
        {
            var (group, _) = distances[i];
            if (i == 0)
            {
                // �ŒZ�����̃O���[�v��Attack�t�F�[�Y
                group.SetPhase(EnemyPhase.Attack, GetClosestPlayerGroup(group));
            }
            else if (i == 1)
            {
                // 2�ԖڂɍŒZ�����̃O���[�v��CollectItem�t�F�[�Y
                group.SetPhase(EnemyPhase.CollectItem, GetClosestItem(group));
            }
            else
            {
                // �c��̃O���[�v��Wait�t�F�[�Y
                group.SetPhase(EnemyPhase.Wait, null);
            }
        }
    }

    List<TGroup> ClusterGroups<TGroup>(List<Transform> objects, int clusterCount) where TGroup : Group, new()
    {
        return ClusterObjects(objects, clusterCount)
            .Select(cluster => new TGroup { Members = cluster.Value }).ToList();
    }

    Dictionary<int, List<Transform>> ClusterObjects(List<Transform> objects, int clusterCount)
    {
        // �����̃Z���g���C�h�������_���ɑI��
        List<Vector3> centroids = new List<Vector3>();
        for (int i = 0; i < clusterCount; i++)
        {
            // objects�̒�����A�����_���ɑI���������̂��Z���g���C�h�Ƃ��Ēǉ�
            centroids.Add(objects[Random.Range(0, objects.Count)].position);
        }

        Dictionary<int, List<Transform>> clusters = new Dictionary<int, List<Transform>>();
        bool hasChanged;

        do
        {
            // �N���X�^���N���A
            clusters.Clear();
            for (int i = 0; i < clusterCount; i++)
            {
                clusters[i] = new List<Transform>();
            }

            // �e�I�u�W�F�N�g���ł��߂��Z���g���C�h�̃N���X�^�Ɋ��蓖��
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
            // �e�N���X�^�̐V�����Z���g���C�h���v�Z
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
        } while (hasChanged); // �Z���g���C�h���ς��Ȃ��Ȃ�܂ŌJ��Ԃ�

        return clusters;
    }

    Vector3 ComputeCentroid(List<Transform> points)
    {
        // �N���X�^���̃I�u�W�F�N�g�̒��S���v�Z
        Vector3 centroid = Vector3.zero;
        foreach (var point in points)
        {
            centroid += point.position;
        }
        return centroid / points.Count;
    }

    Transform GetClosestPlayerGroup(EnemyGroup enemyGroup)
    {
        // �ł��߂��v���C���[�O���[�v���擾
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

    Transform GetClosestItem(EnemyGroup group)
    {
        // �ł��߂��A�C�e�����擾
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
