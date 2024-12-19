using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QEnemyAgent : MonoBehaviour
{
    private int numStates = 8; // 8�̗̈�
    private int numActions = 8; // 8�����̈ړ�
    private float[,] QTable;
    private float alpha = 0.1f; // �w�K��
    private float gamma = 0.9f; // ������
    private float epsilon = 0.1f; // ��-�O���[�f�B�@�̒T����
    [SerializeField] private bool isTraining = true; // �P�����[�h���ǂ���
    private string QTableFilePath; // Q�e�[�u���̕ۑ��t�@�C���p�X
    private Transform targetPlayer; // �^�[�Q�b�g�ƂȂ�v���C���[

    void Start()
    {
        QTable = new float[numStates, numActions];
        QTableFilePath = Path.Combine(Application.dataPath, "Scripts/Character/Enemy/Q.txt");
        if (!isTraining)
        {
            LoadQTable(QTableFilePath);
        }
    }

    void Update()
    {
        FindClosestPlayer();
        if (targetPlayer != null)
        {
            int currentState = GetCurrentState();
            int action = ChooseAction(currentState);
            float reward = TakeAction(action);
            int nextState = GetCurrentState();
            if (isTraining)
            {
                UpdateQTable(currentState, action, reward, nextState);
                SaveQTable(QTableFilePath);
            }
        }
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }

        targetPlayer = closestPlayer;
    }

    int GetCurrentState()
    {
        if (targetPlayer == null)
        {
            return 0;
        }

        Vector3 directionToPlayer = targetPlayer.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        angle = (angle + 22.5f) % 360; // 22.5�x��]������

        if (angle < 0)
        {
            angle += 360;
        }

        int state = Mathf.FloorToInt(angle / 45f); // 45�x���Ƃɕ���
        return state;
    }

    int ChooseAction(int state)
    {
        if (isTraining && Random.value < epsilon)
        {
            // �����_���ȍs����I���i�T���j
            return Random.Range(0, numActions);
        }
        else
        {
            // �ő��Q�l�����s����I���i���p�j
            float maxQ = float.MinValue;
            int bestAction = 0;
            for (int i = 0; i < numActions; i++)
            {
                if (QTable[state, i] > maxQ)
                {
                    maxQ = QTable[state, i];
                    bestAction = i;
                }
            }
            return bestAction;
        }
    }

    float TakeAction(int action)
    {
        // �s�������s���A��V���v�Z
        float angle = action * 45f; // 45�x���Ƃɕ���
        Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        float moveSpeed = 1.0f; // �ړ����x��ݒ�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        Debug.Log("Moving in direction: " + moveDirection + " with action: " + action);

        // �v���C���[�Ƃ̋����Ɋ�Â��ĕ�V���v�Z
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
        float reward = -distanceToPlayer; // �������߂��قǕ�V������

        return reward;
    }
    void UpdateQTable(int state, int action, float reward, int nextState)
    {
        float maxQNext = float.MinValue;
        for (int i = 0; i < numActions; i++)
        {
            if (QTable[nextState, i] > maxQNext)
            {
                maxQNext = QTable[nextState, i];
            }
        }
        QTable[state, action] = QTable[state, action] + alpha * (reward + gamma * maxQNext - QTable[state, action]);
    }

    void SaveQTable(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < numStates; i++)
            {
                for (int j = 0; j < numActions; j++)
                {
                    writer.Write(QTable[i, j] + " ");
                }
                writer.WriteLine();
            }
        }
        Debug.Log("QTable saved to " + filePath);
    }

    void LoadQTable(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                for (int i = 0; i < numStates; i++)
                {
                    string[] line = reader.ReadLine().Split(' ');
                    for (int j = 0; j < numActions; j++)
                    {
                        QTable[i, j] = float.Parse(line[j]);
                    }
                }
            }
        }
    }
}
