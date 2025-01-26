using UnityEngine;

[CreateAssetMenu(fileName = "AudioVolumeData", menuName = "Audio/VolumeData")]
public class AudioVolumeData : ScriptableObject
{
    [Range(-30, 20)] public float bgmVolume = 0f; // ‰Šú’l‚Í0dBiAudioMixer‚Ì”ÍˆÍj
    [Range(-30, 20)] public float seVolume = 0f;
}
