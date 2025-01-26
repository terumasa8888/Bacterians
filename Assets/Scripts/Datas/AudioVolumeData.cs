using UnityEngine;

[CreateAssetMenu(fileName = "AudioVolumeData", menuName = "Audio/VolumeData")]
public class AudioVolumeData : ScriptableObject
{
    [Range(-30, 20)] public float bgmVolume = 0f; // �����l��0dB�iAudioMixer�͈̔́j
    [Range(-30, 20)] public float seVolume = 0f;
}
