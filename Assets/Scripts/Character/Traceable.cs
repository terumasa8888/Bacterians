using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// �ǐՂ𐧌䂷��X�N���v�g
/// </summary>
public class Traceable : MonoBehaviour {

    private NavMeshAgent2D nav;       
    [SerializeField][Tag] private string targetTag;

    private void Start() {
        nav = GetComponent<NavMeshAgent2D>();
    }

    private void Update() {
        Trace();
    }

    /// <summary>
    /// ��ԋ߂��̃^�[�Q�b�g��ǐՂ���
    /// </summary>
    private void Trace() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length == 0) return;

        float nearestDistance = float.MaxValue;
        GameObject finalTarget = null;
        foreach (GameObject target in targets) {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (nearestDistance > distance) {
                nearestDistance = distance;           
                finalTarget = target;
            }
        }

        if (finalTarget == null) return;

        nav.SetDestination(finalTarget.transform.position);
    }
}
