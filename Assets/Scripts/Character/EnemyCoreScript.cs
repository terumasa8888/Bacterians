using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCoreScript : MonoBehaviour {

    public float hp;
    public GameObject clearUI;

    void Start() {

    }

    void Update() {
        //Debug.Log(hp);
        if (hp <= 0) {
            Destroy(this.gameObject);
            Debug.Log("Ÿ‚¿");
            clearUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
