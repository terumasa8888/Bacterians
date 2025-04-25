using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;
    [SerializeField] private AudioVolumeData volumeData;

    private void Start()
    {
        // ScriptableObject�̒l��AudioMixer��������
        audioMixer.SetFloat("BGM", volumeData.bgmVolume);
        audioMixer.SetFloat("SE", volumeData.seVolume);

        // �X���C�_�[�����݂���ꍇ�̂ݏ�����
        if (bgmSlider != null)
        {
            bgmSlider.value = volumeData.bgmVolume;
        }

        if (seSlider != null)
        {
            seSlider.value = volumeData.seVolume;
        }
    }

    public void SetBGMVolume(float volume)
    {
        SetAudioMixerVolume("BGM", volume);
        volumeData.bgmVolume = volume;
    }

    public void SetSEVolume(float volume)
    {
        SetAudioMixerVolume("SE", volume);
        volumeData.seVolume = volume;
    }

    private void SetAudioMixerVolume(string parameterName, float volume)
    {
        if (volume <= -30)
        {
            audioMixer.SetFloat(parameterName, -80); // �~���[�g
        }
        else
        {
            audioMixer.SetFloat(parameterName, volume);
        }
    }
}
