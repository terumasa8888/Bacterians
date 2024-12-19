using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMの音量を変更するスクリプト
/// </summary>
public class BGM : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
