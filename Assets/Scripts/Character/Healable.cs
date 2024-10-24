using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

/// <summary>
/// 近くの味方を回復する機能を提供するクラス
/// </summary>
public class Healable : MonoBehaviour
{
    private float timer = 0;
    private float distance;
    private int healPower;
    private Status status;

    [SerializeField] private float healDistance = 2;
    [SerializeField] private float healInterval = 10f;
    [SerializeField][Tag] private string targetTag;
    [SerializeField] private ParticleSystem healEffect; // 回復エフェクトのプレハブ
    [SerializeField] private GameObject healSoundPrefab; // 回復音のプレハブ

    private SoundPlayer soundPlayer;

    void Start()
    {
        healPower = GetComponent<Status>().HealPower;
        timer = healInterval;
        soundPlayer = GetComponent<SoundPlayer>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0) return;

        Heal();
    }

    /// <summary>
    /// 近くにいる味方を回復する
    /// </summary>
    private void Heal()
    {
        timer = healInterval;
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length == 0) return;

        foreach (GameObject target in targets)
        {
            distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < healDistance)
            {
                target.GetComponent<Status>().Heal(healPower);
                Instantiate(healEffect, target.transform.position, Quaternion.identity);
                soundPlayer.PlaySound(healSoundPrefab);
            }
        }
    }
}
