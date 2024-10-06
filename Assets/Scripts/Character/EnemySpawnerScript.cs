using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// �G�𐶐�����X�|�i�[�̃X�N���v�g
/// </summary>
public class EnemySpawnerScript : MonoBehaviour {
    [SerializeField] private GameObject enemy; // �G�̃v���n�u
    [SerializeField] private float spawnRadius = 2f; // �����ʒu�̃����_���͈�

    void Start() {
        Create();
    }

    void Create() {
        for (int i = 0; i < 100; i++) {
            Vector3 randomPosition = GetRandomPosition();
            var o1 = Instantiate(enemy, randomPosition, Quaternion.identity) as GameObject;
            o1.name = enemy.name + "(" + (i + 1) + ")";
        }
    }

    Vector3 GetRandomPosition() {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, randomCircle.y, 0);
        randomPosition += transform.position;
        return randomPosition;
    }
}
