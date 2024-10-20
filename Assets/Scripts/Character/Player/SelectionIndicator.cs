using UnityEngine;

/// <summary>
/// キャラクター選択のビジュアル表示を担当するスクリプト
/// </summary>
public class SelectionIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicatorSprite;

    void Start()
    {
        // 初期状態ではスプライトを非表示にする
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

