using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QEnemyAgent : MonoBehaviour
{
    private int numStates = 8; // 8つの領域
    private int numActions = 8; // 8方向の移動
    private float[,] QTable;
    private float alpha = 0.1f; // 学習率
    private float gamma = 0.9f; // 割引率
    private float epsilon = 0.1f; // ε-グリーディ法の探索率
    [SerializeField] private bool isTraining = true; // 訓練モードかどうか
    private string QTableFilePath; // Qテーブルの保存ファイルパス
    private Transform targetPlayer; // ターゲットとなるプレイヤー

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
        angle = (angle + 22.5f) % 360; // 22.5度回転させる

        if (angle < 0)
        {
            angle += 360;
        }

        int state = Mathf.FloorToInt(angle / 45f); // 45度ごとに分割
        return state;
    }

    int ChooseAction(int state)
    {
        if (isTraining && Random.value < epsilon)
        {
            // ランダムな行動を選択（探索）
            return Random.Range(0, numActions);
        }
        else
        {
            // 最大のQ値を持つ行動を選択（利用）
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
        // 行動を実行し、報酬を計算
        float angle = action * 45f; // 45度ごとに分割
        Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        float moveSpeed = 1.0f; // 移動速度を設定
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        Debug.Log("Moving in direction: " + moveDirection + " with action: " + action);

        // プレイヤーとの距離に基づいて報酬を計算
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
        float reward = -distanceToPlayer; // 距離が近いほど報酬が高い

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
