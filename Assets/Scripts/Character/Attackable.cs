using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃を行う機能を提供するクラス
/// </summary>
public class Attackable : MonoBehaviour {

    private float attackInterval = 2f;
    private float timer;
    private bool targetInRange;
    private GameObject currentTarget;

    [SerializeField][Tag] private string targetTag;//なくしてもいいかも
    private int attackPower;

    void Start() {
        timer = attackInterval;
        targetInRange = false;
        attackPower = GetComponent<Status>().Attack;
    }

    void Update() {
        timer -= Time.deltaTime;

        if (!targetInRange || timer > 0f) return;
        if (currentTarget == null) return;

        Attack(currentTarget);
        timer = attackInterval;
    }

    void OnCollisionStay2D(Collision2D collision) {
        GameObject collidedObject = collision.gameObject;
        if (!collidedObject.CompareTag(targetTag)) return;

        targetInRange = true;
        currentTarget = collidedObject;
    }

    void OnCollisionExit2D(Collision2D collision) {
        GameObject collidedObject = collision.gameObject;
        if (!collidedObject.CompareTag(targetTag)) return;

        targetInRange = false;
        currentTarget = null;
    }

    void Attack(GameObject target) {
        target.GetComponent<Status>().TakeDamage(attackPower);
    }
}
