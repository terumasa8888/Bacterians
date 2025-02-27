using UnityEngine;

[CreateAssetMenu(fileName = "AudioVolumeData", menuName = "Audio/VolumeData")]
public class AudioVolumeData : ScriptableObject
{
    [Range(-30, 20)] public float bgmVolume = 0f; // 初期値は0dB（AudioMixerの範囲）
    [Range(-30, 20)] public float seVolume = 0f;
}
