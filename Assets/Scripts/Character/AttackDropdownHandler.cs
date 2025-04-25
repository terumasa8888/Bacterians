using UnityEngine;
using UnityEngine.UI;

public class AttackDropdownHandler : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private int defaultValue = 0; // �f�t�H���g�̍U����@�̃C���f�b�N�X
    private Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdown.value = defaultValue;
        dropdown.RefreshShownValue();

        UpdateAttackType();
    }

    /// <summary>
    /// �v���_�E�����j���[�̒l���ύX���ꂽ�Ƃ��ɌĂяo�����
    /// </summary>
    public void UpdateAttackType()
    {
        // Dropdown��Value�ɉ����čU����@��ύX
        switch (dropdown.value)
        {
            case 0:
                characterData.AttackType = AttackType.NormalAttack;
                break;
            case 1:
                characterData.AttackType = AttackType.ExplosionAttack;
                break;
            case 2:
                characterData.AttackType = AttackType.RotatingAttack;
                break;
            case 3:
                characterData.AttackType = AttackType.None;
                break;
            default:
                Debug.LogError("�����ȑI�������I�΂�܂����B");
                return;
        }

        Debug.Log($"�L�����N�^�[ {characterData.ButtonType} �̍U���^�C�v�� {characterData.AttackType} �ɕύX����܂����B");
    }
}
