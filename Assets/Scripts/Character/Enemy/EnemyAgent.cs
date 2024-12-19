using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyAgent : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float epsilon = 0.01f; // εの確率でランダムに行動
    [SerializeField] private float alpha = 0.1f; // 学習率
    [SerializeField] private float gamma = 0.9f; // 割引率
    private int actionCount = 8; // 8方向の行動数
    private float[,] qTable; // Qテーブル
    private string qTableFilePath; // Qテーブルの保存ファイルパス
    [SerializeField] private bool isTraining = true; // 学習モードのフラグ
    private Transform targetPlayer; // ターゲットとなるプレイヤー

    private static List<(int state, int action, float reward, int newState)> replayBuffer = new List<(int, int, float, int)>();
    private static int replayBufferSize = 1000; // バッファの最大サイズ


    void Start()
    {
        qTableFilePath = Path.Combine(Application.dataPath, "Scripts/Character/Enemy/qTable.txt");
        qTable = new float[256, actionCount]; // 8ビットの状態 × 8方向の行動

        // Qテーブルをファイルから読み込む
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
                // 経験をリプレイバッファに追加
                AddToReplayBuffer(state, action, reward, newState);

                // リプレイバッファからランダムにサンプルを取得してQテーブルを更新
                UpdateQTableFromReplayBuffer();

                // Qテーブルをファイルに保存
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

        // 一定の確率でランダムなプレイヤーをターゲットにする
        if (Random.value < 0.2f && players.Length > 0) // 20%の確率でランダムなプレイヤーを選択
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
                state |= (1 << i); // iビット目を1に設定
            }
        }
        return state;
    }

    int ChooseAction(int state)
    {
        if (isTraining && Random.value < epsilon)
        {
            return Random.Range(0, actionCount); // ランダムに行動選択
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

            // 複数の最適な行動がある場合、ランダムに選択
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
        // プレイヤーに近づいていれば報酬、遠ざかれば減点、接触時には高い報酬
        if (Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player")) != null)
        {
            return 10.0f; // プレイヤーに接触で高い報酬
        }
        else if ((newState & 1) == 1)
        {
            return 1.0f; // プレイヤーの方向に近づいた場合に報酬を増加
        }
        else
        {
            return -0.1f; // プレイヤーから遠ざかった場合に減点
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
                    // Debug.Log($"qTable[{i}, {j}] = {qTable[i, j]}"); // ログ出力を削除
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
                qTable[i, j] = 0.0f; // 初期値を設定
            }
        }
    }

    void UpdateQTableFromReplayBuffer()
    {
        int sampleSize = Mathf.Min(10, replayBuffer.Count); // サンプルサイズを設定
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
            replayBuffer.RemoveAt(0); // 古い経験を削除
        }
        replayBuffer.Add((state, action, reward, newState));
    }


}
