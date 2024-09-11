using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    public float hp;

    void Start() {

    }

    void Update() {
        //Debug.Log(hp);
        if (hp <= 0) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in players) {
                obj.GetComponent<StandScript>().attack *= 3;
            }
            Destroy(this.gameObject);
            Debug.Log("ƒAƒCƒeƒ€‚ð‰ó‚µ‚½‚æ");
        }
    }
}
