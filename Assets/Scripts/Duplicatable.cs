using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 増殖する機能を提供するクラス
/// <summary>
public class Duplicatable : MonoBehaviour {
    private int duplicatableNumber;
    private float duplicateInterval;
    private float timer;
    private Status status;

    void Start() {
        status = GetComponent<Status>();
        duplicatableNumber = status.DuplicatableNumber;
        duplicateInterval = status.DuplicateInterval;
        timer = duplicateInterval;
    }

    void Update() {
        if (duplicatableNumber <= 0) return;

        timer -= Time.deltaTime;
        if (timer > 0) return;

        Duplicate();
    }

    private void Duplicate() {
        timer = duplicateInterval;

        GameObject clone = Instantiate(this.gameObject);
        clone.name = this.gameObject.name + "(Clone)";

        status.ReduceDuplicatableNumber();
        duplicatableNumber = status.DuplicatableNumber;
    }
}
