using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン遷移時にTimeScaleを1にする
/// シーン遷移時はTimeScaleがなぜか0のままになるため
/// 原因1.前のシーンで Time.timeScale が 0 に設定されている？
/// 原因2.非同期シーンロード（SceneManager.LoadSceneAsync）を使用している
/// </summary>

public class LoadManager : MonoBehaviour
{
    void Start() {
        Time.timeScale = 1f;
    }
}
