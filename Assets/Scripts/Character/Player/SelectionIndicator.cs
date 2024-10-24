using UnityEngine;

/// <summary>
/// �L�����N�^�[�I���̃r�W���A���\����S������X�N���v�g
/// </summary>
public class SelectionIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicatorSprite;

    void Start()
    {
        indicatorSprite.SetActive(false);
    }

    public void Show()
    {
        indicatorSprite.SetActive(true);
    }

    public void Hide()
    {
        indicatorSprite.SetActive(false);
    }
}

