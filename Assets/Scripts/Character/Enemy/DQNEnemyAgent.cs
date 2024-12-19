using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics; // �ǉ�
using System;
using System.Linq;
using System.Threading.Tasks; // �ǉ�

public class DQNEnemyAgent : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float epsilon = 0.01f; // �Â̊m���Ń����_���ɍs��
    [SerializeField] private float alpha = 0.1f; // �w�K��
    [SerializeField] private float gamma = 0.9f; // ������
    private int actionCount = 8; // 8�����̍s����
    private string modelFilePath; // ���f���̕ۑ��t�@�C���p�X
    [SerializeField] private bool isTraining = true; // �w�K���[�h�̃t���O
    private Transform targetPlayer; // �^�[�Q�b�g�ƂȂ�v���C���[

    private static List<(float[] state, int action, float reward, float[] newState)> replayBuffer = new List<(float[], int, float, float[])>();
    private static int replayBufferSize = 1000; // �o�b�t�@�̍ő�T�C�Y

    private string pythonExePath = @"C:\Users\nakamuraT2020\AppData\Local\Programs\Python\Python38\python.exe"; // Python���s�t�@�C���̃p�X
    private string pythonScriptPath = "Assets/Scripts/Character/Enemy/model.py"; // Python�X�N���v�g�̃p�X

    private int frameCounter = 0; // �t���[���J�E���^�[

    void Start()
    {
        modelFilePath = Path.Combine(Application.dataPath, "Scripts/Character/Enemy/model.h5");

        // ���f���̏�����
        _ = ExecutePythonScriptAsync("initialize_model", actionCount.ToString());
        LoadModel();
    }

    void Update()
    {
        frameCounter++;
        if (frameCounter % 10 == 0) // 10�t���[�����ƂɎ��s
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
    ///  �s����I�����Ď��s����
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
            // �o�������v���C�o�b�t�@�ɒǉ�
            AddToReplayBuffer(state, action, reward, newState);

            // ���̃t���[�����ƂɃ��f�����X�V
            if (frameCounter % 100 == 0) // �Ⴆ��100�t���[�����ƂɍX�V
            {
                // ���v���C�o�b�t�@���烉���_���ɃT���v�����擾���ă��f�����X�V
                UpdateModelFromReplayBuffer();

                // ���f�����t�@�C���ɕۑ�
                SaveModel();
            }
        }
    }


    /// <summary>
    /// �ł��߂��v���C���[��T��
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

        // ���̊m���Ń����_���ȃv���C���[���^�[�Q�b�g�ɂ���
        if (UnityEngine.Random.value < 0.2f && players.Length > 0) // 20%�̊m���Ń����_���ȃv���C���[��I��
        {
            closestPlayer = players[UnityEngine.Random.Range(0, players.Length)].transform;
        }

        targetPlayer = closestPlayer;
    }

    /// <summary>
    /// ���݂̏�Ԃ��擾����
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
    /// �s����I������
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
                return randomAction; // �����_���ɍs���I��
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
                    return fallbackAction; // �p�[�X�Ɏ��s�����ꍇ�̓����_���ɍs���I��
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
    /// �G�[�W�F���g���ړ�������
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
    /// ��V���v�Z����
    /// </summary>
    /// <param name="newState"></param>
    /// <returns></returns>
    float CalculateReward(float[] newState)
    {
        // �v���C���[�ɋ߂Â��Ă���Ε�V�A��������Ό��_�A�ڐG���ɂ͍�����V
        if (Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player")) != null)
        {
            return 10.0f; // �v���C���[�ɐڐG�ō�����V
        }
        else if (newState[0] == 1)
        {
            return 1.0f; // �v���C���[�̕����ɋ߂Â����ꍇ�ɕ�V�𑝉�
        }
        else
        {
            return -0.1f; // �v���C���[���牓���������ꍇ�Ɍ��_
        }
    }

    void UpdateModelFromReplayBuffer()
    {
        if (replayBuffer.Count < replayBufferSize)
        {
            return;
        }

        int sampleSize = 32; // �T���v���T�C�Y��ݒ�
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
            replayBuffer.RemoveAt(0); // �Â��o�����폜
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
        UnityEngine.Debug.Log("Executing Python script with arguments: " + arguments); // ���������O�ɏo��

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
