using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 音を再生する機能を提供するクラス
/// </summary>
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private int maxSimultaneousSounds = 5; // 同時に再生される音の最大数
    private static int currentSoundCount = 0; // 現在再生中の音の数

    /// <summary>
    /// 指定された音のプレハブを再生する
    /// </summary>
    public void PlaySound(GameObject soundEffectPrefab)
    {
        // 同時再生数が上限に達していない場合のみ再生
        if (currentSoundCount < maxSimultaneousSounds)
        {
            GameObject audioObject = Instantiate(soundEffectPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            audioSource.Play();
            currentSoundCount++;
            Destroy(audioObject, audioSource.clip.length / 2);
            DecreaseSoundCountAsync(audioSource.clip.length / 2);
        }
    }

    /// <summary>
    /// 音が鳴り終わるのを待ってから、currentSoundCountを減らす
    /// </summary>
    private async void DecreaseSoundCountAsync(float delay)
    {
        await Task.Delay((int)(delay * 1000));
        currentSoundCount--;
    }
}
