using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyAgent : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float epsilon = 0.01f; // �Â̊m���Ń����_���ɍs��
    [SerializeField] private float alpha = 0.1f; // �w�K��
    [SerializeField] private float gamma = 0.9f; // ������
    private int actionCount = 8; // 8�����̍s����
    private float[,] qTable; // Q�e�[�u��
    private string qTableFilePath; // Q�e�[�u���̕ۑ��t�@�C���p�X
    [SerializeField] private bool isTraining = true; // �w�K���[�h�̃t���O
    private Transform targetPlayer; // �^�[�Q�b�g�ƂȂ�v���C���[

    private static List<(int state, int action, float reward, int newState)> replayBuffer = new List<(int, int, float, int)>();
    private static int replayBufferSize = 1000; // �o�b�t�@�̍ő�T�C�Y


    void Start()
    {
        qTableFilePath = Path.Combine(Application.dataPath, "Scripts/Character/Enemy/qTable.txt");
        qTable = new float[256, actionCount]; // 8�r�b�g�̏�� �~ 8�����̍s��

        // Q�e�[�u�����t�@�C������ǂݍ���
        LoadQTable();
    }

    void Update()
    {
        FindClosestPlayer();
        if (targetPlayer != null)
        {
            int state = GetState();
            int action = ChooseAction(state);
            MoveAgent(action);
            int newState = GetState();
            float reward = CalculateReward(newState);

            if (isTraining)
            {
                // �o�������v���C�o�b�t�@�ɒǉ�
                AddToReplayBuffer(state, action, reward, newState);

                // ���v���C�o�b�t�@���烉���_���ɃT���v�����擾����Q�e�[�u�����X�V
                UpdateQTableFromReplayBuffer();

                // Q�e�[�u�����t�@�C���ɕۑ�
                SaveQTable();
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

        // ���̊m���Ń����_���ȃv���C���[���^�[�Q�b�g�ɂ���
        if (Random.value < 0.2f && players.Length > 0) // 20%�̊m���Ń����_���ȃv���C���[��I��
        {
            closestPlayer = players[Random.Range(0, players.Length)].transform;
        }

        targetPlayer = closestPlayer;
    }

    int GetState()
    {
        int state = 0;
        for (int i = 0; i < actionCount; i++)
        {
            float angle = i * (360f / actionCount);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, LayerMask.GetMask("Player"));

            if (hit.collider != null)
            {
                state |= (1 << i); // i�r�b�g�ڂ�1�ɐݒ�
            }
        }
        return state;
    }

    int ChooseAction(int state)
    {
        if (isTraining && Random.value < epsilon)
        {
            return Random.Range(0, actionCount); // �����_���ɍs���I��
        }
        else
        {
            float maxQ = float.MinValue;
            int bestAction = 0;
            List<int> bestActions = new List<int>();

            for (int i = 0; i < actionCount; i++)
            {
                if (qTable[state, i] > maxQ)
                {
                    maxQ = qTable[state, i];
                    bestActions.Clear();
                    bestActions.Add(i);
                }
                else if (qTable[state, i] == maxQ)
                {
                    bestActions.Add(i);
                }
            }

            // �����̍œK�ȍs��������ꍇ�A�����_���ɑI��
            if (bestActions.Count > 1)
            {
                bestAction = bestActions[Random.Range(0, bestActions.Count)];
            }
            else
            {
                bestAction = bestActions[0];
            }

            return bestAction;
        }
    }

    void MoveAgent(int action)
    {
        float angle = action * (360f / actionCount);
        Vector3 moveDirection = Quaternion.Euler(0, 0, angle) * Vector3.up;
        transform.position += moveDirection * Time.deltaTime;
        Debug.Log("Moving in direction: " + moveDirection + " with action: " + action);
    }

    float CalculateReward(int newState)
    {
        // �v���C���[�ɋ߂Â��Ă���Ε�V�A��������Ό��_�A�ڐG���ɂ͍�����V
        if (Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player")) != null)
        {
            return 10.0f; // �v���C���[�ɐڐG�ō�����V
        }
        else if ((newState & 1) == 1)
        {
            return 1.0f; // �v���C���[�̕����ɋ߂Â����ꍇ�ɕ�V�𑝉�
        }
        else
        {
            return -0.1f; // �v���C���[���牓���������ꍇ�Ɍ��_
        }
    }

    void UpdateQTable(int state, int action, float reward, int newState)
    {
        float maxQ = float.MinValue;
        for (int i = 0; i < actionCount; i++)
        {
            if (qTable[newState, i] > maxQ)
            {
                maxQ = qTable[newState, i];
            }
        }
        float oldQValue = qTable[state, action];
        qTable[state, action] += alpha * (reward + gamma * maxQ - qTable[state, action]);
        Debug.Log($"Updated qTable[{state}, {action}] from {oldQValue} to {qTable[state, action]}");
    }

    void SaveQTable()
    {
        using (StreamWriter writer = new StreamWriter(qTableFilePath))
        {
            for (int i = 0; i < qTable.GetLength(0); i++)
            {
                for (int j = 0; j < qTable.GetLength(1); j++)
                {
                    writer.Write(qTable[i, j] + " ");
                    // Debug.Log($"qTable[{i}, {j}] = {qTable[i, j]}"); // ���O�o�͂��폜
                }
                writer.WriteLine();
            }
        }
        Debug.Log("QTable saved to " + qTableFilePath);
    }

    void LoadQTable()
    {
        if (File.Exists(qTableFilePath))
        {
            using (StreamReader reader = new StreamReader(qTableFilePath))
            {
                for (int i = 0; i < qTable.GetLength(0); i++)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        Debug.LogWarning("QTable file is incomplete, initializing remaining values with default.");
                        InitializeQTableFromIndex(i);
                        return;
                    }

                    string[] values = line.Trim().Split(' ');
                    if (values.Length != qTable.GetLength(1))
                    {
                        Debug.LogError($"QTable file format is incorrect at line {i}. Expected {qTable.GetLength(1)} values, but got {values.Length}.");
                        InitializeQTableFromIndex(i);
                        return;
                    }

                    for (int j = 0; j < qTable.GetLength(1); j++)
                    {
                        if (float.TryParse(values[j], out float result))
                        {
                            qTable[i, j] = result;
                        }
                        else
                        {
                            Debug.LogError($"Failed to parse QTable value at [{i}, {j}]: {values[j]}");
                            InitializeQTableFromIndex(i);
                            return;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("QTable file not found, initializing with default values.");
            InitializeQTableFromIndex(0);
        }
    }

    void InitializeQTableFromIndex(int startIndex)
    {
        for (int i = startIndex; i < qTable.GetLength(0); i++)
        {
            for (int j = 0; j < qTable.GetLength(1); j++)
            {
                qTable[i, j] = 0.0f; // �����l��ݒ�
            }
        }
    }

    void UpdateQTableFromReplayBuffer()
    {
        int sampleSize = Mathf.Min(10, replayBuffer.Count); // �T���v���T�C�Y��ݒ�
        for (int i = 0; i < sampleSize; i++)
        {
            int index = Random.Range(0, replayBuffer.Count);
            var (state, action, reward, newState) = replayBuffer[index];

            float maxQ = float.MinValue;
            for (int j = 0; j < actionCount; j++)
            {
                if (qTable[newState, j] > maxQ)
                {
                    maxQ = qTable[newState, j];
                }
            }
            qTable[state, action] += alpha * (reward + gamma * maxQ - qTable[state, action]);
        }
    }

    void AddToReplayBuffer(int state, int action, float reward, int newState)
    {
        if (replayBuffer.Count >= replayBufferSize)
        {
            replayBuffer.RemoveAt(0); // �Â��o�����폜
        }
        replayBuffer.Add((state, action, reward, newState));
    }


}
