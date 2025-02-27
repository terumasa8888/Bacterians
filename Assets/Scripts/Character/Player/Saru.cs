using UnityEngine;

public class Saru : MonoBehaviour
{
    public IStatus Status { get; private set; }
    private AttackBehaviour attackBehaviour;

    void Awake()
    {
        Status = GetComponent<IStatus>();
        attackBehaviour = GetComponent<NormalAttack>();//ここをExplodeに変えれば、振る舞いもかわる
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            attackBehaviour?.Attack(collision.gameObject);//NormalAttackに委譲
        }
    }
}
