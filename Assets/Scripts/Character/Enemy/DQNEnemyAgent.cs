using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics; // 追加
using System;
using System.Linq;
using System.Threading.Tasks; // 追加

public class DQNEnemyAgent : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float epsilon = 0.01f; // εの確率でランダムに行動
    [SerializeField] private float alpha = 0.1f; // 学習率
    [SerializeField] private float gamma = 0.9f; // 割引率
    private int actionCount = 8; // 8方向の行動数
    private string modelFilePath; // モデルの保存ファイルパス
    [SerializeField] private bool isTraining = true; // 学習モードのフラグ
    private Transform targetPlayer; // ターゲットとなるプレイヤー

    private static List<(float[] state, int action, float reward, float[] newState)> replayBuffer = new List<(float[], int, float, float[])>();
    private static int replayBufferSize = 1000; // バッファの最大サイズ

    private string pythonExePath = @"C:\Users\nakamuraT2020\AppData\Local\Programs\Python\Python38\python.exe"; // Python実行ファイルのパス
    private string pythonScriptPath = "Assets/Scripts/Character/Enemy/model.py"; // Pythonスクリプトのパス

    private int frameCounter = 0; // フレームカウンター

    void Start()
    {
        modelFilePath = Path.Combine(Application.dataPath, "Scripts/Character/Enemy/model.h5");

        // モデルの初期化
        _ = ExecutePythonScriptAsync("initialize_model", actionCount.ToString());
        LoadModel();
    }

    void Update()
    {
        frameCounter++;
        if (frameCounter % 10 == 0) // 10フレームごとに実行
        {
            FindClosestPlayer();
            if (targetPlayer != null)
            {
                float[] state = GetState();
                ProcessActionAsync(state);
            }
        }
    }

    /// <summary>
    ///  行動を選択して実行する
    /// </summary>
    /// <param name="state"></param>
    async void ProcessActionAsync(float[] state)
    {
        int action = await ChooseAction(state);
        MoveAgent(action);
        float[] newState = GetState();
        float reward = CalculateReward(newState);

        if (isTraining)
        {
            // 経験をリプレイバッファに追加
            AddToReplayBuffer(state, action, reward, newState);

            // 一定のフレームごとにモデルを更新
            if (frameCounter % 100 == 0) // 例えば100フレームごとに更新
            {
                // リプレイバッファからランダムにサンプルを取得してモデルを更新
                UpdateModelFromReplayBuffer();

                // モデルをファイルに保存
                SaveModel();
            }
        }
    }


    /// <summary>
    /// 最も近いプレイヤーを探す
    /// </summary>

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
        if (UnityEngine.Random.value < 0.2f && players.Length > 0) // 20%の確率でランダムなプレイヤーを選択
        {
            closestPlayer = players[UnityEngine.Random.Range(0, players.Length)].transform;
        }

        targetPlayer = closestPlayer;
    }

    /// <summary>
    /// 現在の状態を取得する
    /// </summary>
    /// <returns></returns>
    float[] GetState()
    {
        List<float> state = new List<float>();
        for (int i = 0; i < actionCount; i++)
        {
            float angle = i * (360f / actionCount);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, LayerMask.GetMask("Player"));

            if (hit.collider != null)
            {
                UnityEngine.Debug.Log("Player detected at angle: " + angle);
            }

            state.Add(hit.collider != null ? 1.0f : 0.0f);
        }
        return state.ToArray();
    }

    /// <summary>
    /// 行動を選択する
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    async Task<int> ChooseAction(float[] state)
    {
        try
        {
            if (isTraining && UnityEngine.Random.value < epsilon)
            {
                int randomAction = UnityEngine.Random.Range(0, actionCount);
                UnityEngine.Debug.Log("Choosing random action: " + randomAction);
                return randomAction; // ランダムに行動選択
            }
            else
            {
                string stateStr = string.Join(",", state);
                string result = await ExecutePythonScriptAsync("choose_action", stateStr);

                int action;
                if (int.TryParse(result, out action))
                {
                    UnityEngine.Debug.Log("Choosing action from model: " + action);
                    return action;
                }
                else
                {
                    UnityEngine.Debug.LogError("Failed to parse action from Python script: " + result);
                    int fallbackAction = UnityEngine.Random.Range(0, actionCount);
                    UnityEngine.Debug.Log("Fallback to random action: " + fallbackAction);
                    return fallbackAction; // パースに失敗した場合はランダムに行動選択
                }
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError("Error in ChooseAction: " + ex.Message);
            throw;
        }
    }

    /// <summary>
    /// エージェントを移動させる
    /// </summary>
    /// <param name="action"></param>
    void MoveAgent(int action)
    {
        UnityEngine.Debug.Log("MoveAgent called with action: " + action);
        float angle = action * (360f / actionCount);
        Vector3 moveDirection = Quaternion.Euler(0, 0, angle) * Vector3.up;
        transform.position += 100 * moveDirection * Time.deltaTime;
        UnityEngine.Debug.Log("Moving in direction: " + moveDirection + " with action: " + action);
    }


    /// <summary>
    /// 報酬を計算する
    /// </summary>
    /// <param name="newState"></param>
    /// <returns></returns>
    float CalculateReward(float[] newState)
    {
        // プレイヤーに近づいていれば報酬、遠ざかれば減点、接触時には高い報酬
        if (Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player")) != null)
        {
            return 10.0f; // プレイヤーに接触で高い報酬
        }
        else if (newState[0] == 1)
        {
            return 1.0f; // プレイヤーの方向に近づいた場合に報酬を増加
        }
        else
        {
            return -0.1f; // プレイヤーから遠ざかった場合に減点
        }
    }

    void UpdateModelFromReplayBuffer()
    {
        if (replayBuffer.Count < replayBufferSize)
        {
            return;
        }

        int sampleSize = 32; // サンプルサイズを設定
        var samples = new List<(float[] state, int action, float reward, float[] newState)>();
        for (int i = 0; i < sampleSize; i++)
        {
            int index = UnityEngine.Random.Range(0, replayBuffer.Count);
            samples.Add(replayBuffer[index]);
        }

        foreach (var (state, action, reward, newState) in samples)
        {
            string stateStr = string.Join(",", state);
            string newStateStr = string.Join(",", newState);
            _ = ExecutePythonScriptAsync("update_model", stateStr, action.ToString(), reward.ToString(), newStateStr);
        }
    }

    void AddToReplayBuffer(float[] state, int action, float reward, float[] newState)
    {
        if (replayBuffer.Count >= replayBufferSize)
        {
            replayBuffer.RemoveAt(0); // 古い経験を削除
        }
        replayBuffer.Add((state, action, reward, newState));
    }

    void SaveModel()
    {
        _ = ExecutePythonScriptAsync("save_model", modelFilePath);
        UnityEngine.Debug.Log("Model saved to " + modelFilePath);
    }

    void LoadModel()
    {
        if (File.Exists(modelFilePath))
        {
            _ = ExecutePythonScriptAsync("load_model", modelFilePath);
            UnityEngine.Debug.Log("Model loaded from " + modelFilePath);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Model file not found, initializing with new model.");
        }
    }

    async Task<string> ExecutePythonScriptAsync(params string[] args)
    {
        string arguments = string.Join(" ", args.Prepend(pythonScriptPath));
        UnityEngine.Debug.Log("Executing Python script with arguments: " + arguments); // 引数をログに出力

        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = pythonExePath,
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = await reader.ReadToEndAsync().ConfigureAwait(false);
                UnityEngine.Debug.Log("Python script output: " + result);

                if (string.IsNullOrEmpty(result))
                {
                    using (StreamReader errorReader = process.StandardError)
                    {
                        string error = await errorReader.ReadToEndAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(error))
                        {
                            UnityEngine.Debug.LogError("Python script error: " + error);
                        }
                    }
                }
                return result.Trim();
            }
        }
    }
}
