using UnityEngine;
using UnityEngine.UI;

public class AttackDropdownHandler : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private int defaultValue = 0; // デフォルトの攻撃手法のインデックス
    private Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdown.value = defaultValue;
        dropdown.RefreshShownValue();

        UpdateAttackType();
    }

    /// <summary>
    /// プルダウンメニューの値が変更されたときに呼び出される
    /// </summary>
    public void UpdateAttackType()
    {
        // DropdownのValueに応じて攻撃手法を変更
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
                Debug.LogError("無効な選択肢が選ばれました。");
                return;
        }

        Debug.Log($"キャラクター {characterData.ButtonType} の攻撃タイプが {characterData.AttackType} に変更されました。");
    }
}
