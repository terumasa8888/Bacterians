using UnityEngine;

public class Saru : MonoBehaviour
{
    public IStatus Status { get; private set; }
    private AttackBehaviour attackBehaviour;

    void Awake()
    {
        Status = GetComponent<IStatus>();
        attackBehaviour = GetComponent<NormalAttack>();//‚±‚±‚ğExplode‚É•Ï‚¦‚ê‚ÎAU‚é•‘‚¢‚à‚©‚í‚é
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            attackBehaviour?.Attack(collision.gameObject);//NormalAttack‚ÉˆÏ÷
        }
    }
}
