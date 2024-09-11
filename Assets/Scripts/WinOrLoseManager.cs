using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLoseManager : MonoBehaviour
{
    public GameObject clearUI, loseUI;
    public GameObject bgm;
    AudioSource audioSource;
    private GameObject playerSpawner;
    PlayerSpawnerScript playerSpawnerScript;

    float timer;
    GameObject[] players, enemies;
    GameObject boss;

    void Start()
    {
        playerSpawner = GameObject.Find("PlayerSpawner");
        playerSpawnerScript = playerSpawner.GetComponent<PlayerSpawnerScript>();
        audioSource = bgm.GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 3) {

            players = GameObject.FindGameObjectsWithTag("Player");
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            boss = GameObject.FindGameObjectWithTag("Boss");

            if ((enemies.Length == 0) && (boss == null)) {
                clearUI.SetActive(true);
                audioSource.volume = 0.3f;
                Time.timeScale = 0f;
            }
            if(players.Length == 0 ) {
                int totalCreateTimes = playerSpawnerScript.saruCreateTimes + playerSpawnerScript.houseDustCreateTimes + playerSpawnerScript.clioneCreateTimes + playerSpawnerScript.mijinkoCreateTimes + playerSpawnerScript.piroriCreateTimes;
                if (totalCreateTimes == 0) {
                    loseUI.SetActive(true);
                    audioSource.volume = 0.3f;
                    Time.timeScale = 0f;
                }
            }
            timer = 0;

        }
    }
}
