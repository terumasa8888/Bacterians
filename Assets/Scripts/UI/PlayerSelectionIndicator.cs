using UnityEngine;

/// <summary>
/// �L�����N�^�[�I���̃r�W���A���\����S������X�N���v�g
/// </summary>
public class CharacterSelectionIndicator : MonoBehaviour
{
    [SerializeField] private GameObject circle;

    public void Show(Vector3 mousePosition)
    {
        Vector3 point = Vector3.zero;
        RectTransform rc = circle.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rc, mousePosition, null, out point);

        circle.GetComponent<RectTransform>().position = point;
        circle.SetActive(true);
    }

    public void Hide()
    {
        circle.SetActive(false);
    }
}
