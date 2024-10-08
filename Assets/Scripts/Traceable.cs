using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// �ǐՂ𐧌䂷��X�N���v�g
/// </summary>
public class Traceable : MonoBehaviour {

    private NavMeshAgent2D nav;
    private float distance; // �����ۑ��p
    private float nearestDistance; // �ł��߂��^�[�Q�b�g�̋���        
    private GameObject finalTarget;
    private GameObject[] targets;

    [SerializeField][Tag] private string targetTag; // �ǂ�������Ώۂ̃^�O���C���X�y�N�^�[����ݒ�

    private void Start() {
        nav = GetComponent<NavMeshAgent2D>();
    }

    private void Update() {
        Trace();
    }

    private void Trace() {
        targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length == 0) return;
        nearestDistance = float.MaxValue; // �����������[�v�̊O�Ɉړ�
        foreach (GameObject target in targets) {
            distance = Vector3.Distance(target.transform.position, transform.position);
            if (nearestDistance > distance) {
                nearestDistance = distance; // ������ۑ�            
                finalTarget = target; // �^�[�Q�b�g���X�V
            }
        }

        if (finalTarget == null) return;

        nav.SetDestination(finalTarget.transform.position);
    }
}
