using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// Player, Enemyの数を監視し、勝敗を判定する
/// 音の管理をしているので外部に任せる
/// </summary>
public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject clearUI, loseUI;
    [SerializeField] private GameObject bgm;
    [SerializeField] GameObject playerSpawner;
    AudioSource audioSource;
    private PlayerSpawner playerSpawnerScript;

    GameObject[] players, enemies;
    GameObject boss;

    void Start()
    {
        playerSpawnerScript = playerSpawner.GetComponent<PlayerSpawner>();
        audioSource = bgm.GetComponent<AudioSource>();

        // 3秒ごとに勝敗判定を行う
        Observable.Interval(System.TimeSpan.FromSeconds(3))
            .Subscribe(_ => CheckWinOrLose())
            .AddTo(this);
    }

    /// <summary>
    /// 勝利判定、敗北判定を行う
    /// </summary>
    private void CheckWinOrLose()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        boss = GameObject.FindGameObjectWithTag("Boss");

        // Playerが全滅したら負け
        if (players.Length == 0)
        {
            int totalCreateTimes = playerSpawnerScript.GetTotalCreatableTimes();
            if (totalCreateTimes == 0)
            {
                loseUI.SetActive(true);//UIの処理はほかに譲るべき
                audioSource.volume = 0.3f;
                Time.timeScale = 0f;
                return;
            }
        }

        // 敵が全滅したら勝ち
        if (enemies.Length == 0 && boss == null)
        {
            clearUI.SetActive(true);//UIの処理はほかに譲るべき
            audioSource.volume = 0.3f;
            Time.timeScale = 0f;
            return;
        }
    }
}
