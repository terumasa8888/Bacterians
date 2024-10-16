using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// Player, Enemy‚Ì”‚ğŠÄ‹‚µAŸ”s‚ğ”»’è‚·‚é
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
    /// Ÿ—˜”»’èA”s–k”»’è‚ğs‚¤
    /// </summary>
    private void CheckWinOrLose()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        boss = GameObject.FindGameObjectWithTag("Boss");

        // Player‚ª‘S–Å‚µ‚½‚ç•‰‚¯
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

        // “G‚ª‘S–Å‚µ‚½‚çŸ‚¿
        if (enemies.Length == 0 && boss == null)
        {
            clearUI.SetActive(true);
            audioSource.volume = 0.3f;
            Time.timeScale = 0f;
            return;
        }
    }
}
