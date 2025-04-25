using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clione�L�����N�^�[�N���X
/// </summary>
public class Clione : PlayerBase
{
    private float timer = 0;
    private int healPower;
    private string myTag;

    [SerializeField] private float healDistance = 2;
    [SerializeField] private float healInterval = 10f;
    [SerializeField] private ParticleSystem healEffect; // �񕜃G�t�F�N�g�̃v���n�u
    [SerializeField] private GameObject healSoundPrefab; // �񕜉��̃v���n�u

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
    /// �߂��ɂ��閡�����񕜂���
    /// </summary>
    protected virtual void Heal()
    {
        // �L���b�V�������^�O���g�p���ă^�[�Q�b�g���擾
        GameObject[] targets = GameObject.FindGameObjectsWithTag(myTag);

        if (targets.Length == 0) return;

        foreach (GameObject target in targets)
        {
            // �������g�͉񕜑Ώۂ��珜�O
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
