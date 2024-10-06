using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムのスクリプト
/// hpが0になると、プレイヤーの攻撃力を3倍にする
/// 攻撃力を直接いじってるのがよくない
/// カプセル化するべき
/// </summary>
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
            Debug.Log("アイテムを壊したよ");
        }
    }
}
