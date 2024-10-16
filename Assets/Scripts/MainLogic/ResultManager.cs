using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// Player, Enemy�̐����Ď����A���s�𔻒肷��
/// </summary>
public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject clearUI, loseUI;
    [SerializeField] private GameObject bgm;
    AudioSource audioSource;
    private PlayerSpawner playerSpawnerScript;

    GameObject[] players, enemies;
    GameObject boss;

    void Start()
    {
        GameObject playerSpawnerObject = GameObject.Find("PlayerSpawnerObject");
        playerSpawnerScript = playerSpawnerObject.GetComponent<PlayerSpawner>();
        audioSource = bgm.GetComponent<AudioSource>();

        Observable.Interval(System.TimeSpan.FromSeconds(3))
            .Subscribe(_ => CheckWinOrLose())
            .AddTo(this);
    }

    /// <summary>
    /// ��������A�s�k������s��
    /// </summary>
    private void CheckWinOrLose()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        boss = GameObject.FindGameObjectWithTag("Boss");

        // Player���S�ł����畉��
        if (players.Length == 0)
        {
            int totalCreateTimes = playerSpawnerScript.GetTotalCreatableTimes();
            if (totalCreateTimes == 0)
            {
                loseUI.SetActive(true);
                audioSource.volume = 0.3f;
                Time.timeScale = 0f;
                return;
            }
        }

        // �G���S�ł����珟��
        if (enemies.Length == 0 && boss == null)
        {
            clearUI.SetActive(true);
            audioSource.volume = 0.3f;
            Time.timeScale = 0f;
            return;
        }
    }
}
