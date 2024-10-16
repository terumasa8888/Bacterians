using UnityEngine;

/// <summary>
/// Tilemapの初期化時に非アクティブにするスクリプト
/// これをしないとキャラクターが動けない
/// </summary>
public class DisableOnStart : MonoBehaviour {
    void Start() {
        gameObject.SetActive(false);
    }
}