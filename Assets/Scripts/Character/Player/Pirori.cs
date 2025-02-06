using UnityEngine;

public class Pirori : MonoBehaviour
{
    public IStatus Status { get; private set; }
    private AttackBehaviour attackBehaviour;

    void Awake()
    {
        Status = GetComponent<IStatus>();
        if (Status == null)
        {
            Debug.LogError("IStatus component not found on " + gameObject.name);
        }

        attackBehaviour = GetComponent<Explode>();
        if (attackBehaviour == null)
        {
            Debug.LogError("Explode component not found on " + gameObject.name);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            attackBehaviour?.Attack(collision.gameObject);//ExplodeÇ…àœè˜
        }
        
    }
}
