using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerCoreScript : MonoBehaviour {

    public float hp;

    void Start() {

    }

    void Update() {
        if (hp <= 0) {
            //Destroy(this.gameObject);
            Debug.Log("����");
            //���炷
            //UI�������āA������ʁB���𖳌���
            //SceneMa
        }
    }
}
