using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �G�𐶐�����X�|�i�[�̃X�N���v�g
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemyCount = 50;


    void Start()
    {
        CreateEnemy();
    }

    /// <summary>
    /// Enemy�𐶐�����
    /// </summary>
    private void CreateEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject clone = Instantiate(enemy, transform.position, Quaternion.identity);
            ScatterPosition(clone);
            clone.name = enemy.name + "(" + (i + 1) + ")";
        }
    }

    /// <summary>
    /// �L�����N�^�[�̐����ʒu���U�炷
    /// PlayerSpawnerScript�ɂ��߂�����������
    /// </summary>
    private void ScatterPosition(GameObject character)
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        character.transform.position += new Vector3(x, y, 0);
    }
}
