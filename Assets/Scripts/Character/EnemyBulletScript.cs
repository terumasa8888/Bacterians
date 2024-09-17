using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �{�X�G�̒e(�U�R�G)�̃X�N���v�g
/// ��ʊO�Ŏ����ŏ��ł���
/// </summary>
public class EnemyBulletScript : MonoBehaviour {

    public GameObject hitEffectPrefab;
    public float Velocity_0, theta;
    

    Rigidbody2D rid2d;
    void Start() {
        //Rigidbody�擾
        rid2d = GetComponent<Rigidbody2D>();
        //�p�x���l�����Ēe�̑��x�v�Z
        Vector2 bulletV = rid2d.velocity;
        bulletV.x = Velocity_0 * Mathf.Cos(theta);
        bulletV.y = Velocity_0 * Mathf.Sin(theta);

        rid2d.velocity = bulletV;
    }

    void Update() {

        transform.Rotate(0, 0, 1);

        if ((transform.position.x < -12)|| (transform.position.x > 16) || (transform.position.y < -8) || (transform.position.y > 10)) {
            Destroy(gameObject);
        }

    }
}
