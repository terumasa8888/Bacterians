using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clioneキャラクタークラス
/// </summary>
public class Clione : PlayerBase
{
    private float timer = 0;
    private int healPower;
    private string myTag;

    [SerializeField] private float healDistance = 2;
    [SerializeField] private float healInterval = 10f;
    [SerializeField] private ParticleSystem healEffect; // 回復エフェクトのプレハブ
    [SerializeField] private GameObject healSoundPrefab; // 回復音のプレハブ

    private SoundPlayer soundPlayer;

    protected override void Awake()
    {
        base.Awake();
        healPower = status.HealPower;
        timer = healInterval;
        soundPlayer = GetComponent<SoundPlayer>();
        myTag = gameObject.tag;
    }

    protected override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        if (timer > 0) return;

        Heal();
        timer = healInterval;
    }

    /// <summary>
    /// 近くにいる味方を回復する
    /// </summary>
    protected virtual void Heal()
    {
        // キャッシュしたタグを使用してターゲットを取得
        GameObject[] targets = GameObject.FindGameObjectsWithTag(myTag);

        if (targets.Length == 0) return;

        foreach (GameObject target in targets)
        {
            // 自分自身は回復対象から除外
            if (target == gameObject) continue;

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < healDistance)
            {
                IDamageable t = target.GetComponent<IDamageable>();
                if (t != null)
                {
                    t.Status.Heal(healPower);
                    Instantiate(healEffect, t.GameObject.transform.position, Quaternion.identity);
                    soundPlayer.PlaySound(healSoundPrefab);
                }
            }
        }
    }
}
