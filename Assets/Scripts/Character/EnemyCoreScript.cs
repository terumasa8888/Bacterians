using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Enemy側の核のスクリプト
/// </summary>
public class EnemyCoreScript : MonoBehaviour {

    public float hp;
    public GameObject clearUI;

    void Start() {

    }

    void Update() {
        //Debug.Log(hp);
        if (hp <= 0) {
            Destroy(this.gameObject);
            Debug.Log("勝ち");
            clearUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
