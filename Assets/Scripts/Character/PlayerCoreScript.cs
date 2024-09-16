using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Player側の核のスクリプト
/// </summary>
public class PlayerCoreScript : MonoBehaviour {

    public float hp;

    void Start() {

    }

    void Update() {
        if (hp <= 0) {
            //Destroy(this.gameObject);
            Debug.Log("負け");
            //音鳴らす
            //UIいじって、負け画面。裏を無効化
            //SceneMa
        }
    }
}
