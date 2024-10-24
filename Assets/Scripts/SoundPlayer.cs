using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// �����Đ�����@�\��񋟂���N���X
/// </summary>
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private int maxSimultaneousSounds = 5; // �����ɍĐ�����鉹�̍ő吔
    private static int currentSoundCount = 0; // ���ݍĐ����̉��̐�

    /// <summary>
    /// �w�肳�ꂽ���̃v���n�u���Đ�����
    /// </summary>
    public void PlaySound(GameObject soundEffectPrefab)
    {
        // �����Đ���������ɒB���Ă��Ȃ��ꍇ�̂ݍĐ�
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
    /// ������I���̂�҂��Ă���AcurrentSoundCount�����炷
    /// </summary>
    private async void DecreaseSoundCountAsync(float delay)
    {
        await Task.Delay((int)(delay * 1000));
        currentSoundCount--;
    }
}
