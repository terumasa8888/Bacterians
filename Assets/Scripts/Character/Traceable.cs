using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// 追跡を制御するスクリプト
/// </summary>
public class Traceable : MonoBehaviour {

    private NavMeshAgent2D nav;
    private float distance; // 距離保存用
    private float nearestDistance; // 最も近いターゲットの距離        
    private GameObject finalTarget;
    private GameObject[] targets;

    [SerializeField][Tag] private string targetTag; // 追いかける対象のタグをインスペクターから設定

    private void Start() {
        nav = GetComponent<NavMeshAgent2D>();
    }

    private void Update() {
        Trace();
    }

    private void Trace() {
        targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length == 0) return;
        nearestDistance = float.MaxValue; // 初期化をループの外に移動
        foreach (GameObject target in targets) {
            distance = Vector3.Distance(target.transform.position, transform.position);
            if (nearestDistance > distance) {
                nearestDistance = distance; // 距離を保存            
                finalTarget = target; // ターゲットを更新
            }
        }

        if (finalTarget == null) return;

        nav.SetDestination(finalTarget.transform.position);
    }
}
