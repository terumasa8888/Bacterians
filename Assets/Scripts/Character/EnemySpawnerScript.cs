using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �G�𐶐�����X�|�i�[�̃X�N���v�g
/// </summary>
public class EnemySpawnerScript : MonoBehaviour {
    [SerializeField] private GameObject enemy; // �G�̃v���n�u
    private float spawnRadius = 2f; // �����ʒu�̃����_���͈�

    void Start() {
        Create();
    }

    void Create() {
        for (int i = 0; i < 100; i++) {
            Vector3 randomPosition = GetRandomPosition();
            GameObject clone = Instantiate(enemy, randomPosition, Quaternion.identity);
            clone.name = enemy.name + "(" + (i + 1) + ")";
        }
    }

    Vector3 GetRandomPosition() {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, randomCircle.y, 0);
        randomPosition += transform.position;
        return randomPosition;
    }
}
