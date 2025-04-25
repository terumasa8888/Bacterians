using UnityEngine;
using UniRx;

/// <summary>
/// �A�C�e���N���X
/// </summary>
public class Item : CharacterBase
{
    [SerializeField] private int attackMultiplier = 3; // �U���͂̔{��
    private string lastAttackerTag; // �Ƃǂ߂��h�����L�����N�^�[�̃^�O

    protected override void Awake()
    {
        base.Awake();

        // OnDie�C�x���g���w�ǂ��ăA�C�e���j�󏈗������s
        status.OnDie.Subscribe(_ => HandleOnDie()).AddTo(this);
    }

    /// <summary>
    /// �Փˎ��ɌĂяo����A�U�����Ă����L�����N�^�[�̃^�O���L�^
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastAttackerTag = collision.gameObject.tag;
    }

    /// <summary>
    /// ���S���ɌĂяo����A�Ƃǂ߂��h�����L�����N�^�[�̃^�O�����L�����N�^�[�̍U���͂𑝉�
    /// </summary>
    private void HandleOnDie()
    {
        if (!string.IsNullOrEmpty(lastAttackerTag))
        {
            MultiplyAttack(lastAttackerTag);
            Debug.Log($"Attacker Tag: {lastAttackerTag}");
        }
    }

    /// <summary>
    /// �w�肳�ꂽ�^�O�����L�����N�^�[�̍U���͂𑝉�
    /// </summary>
    private void MultiplyAttack(string attackerTag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(attackerTag);
        foreach (GameObject target in targets)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null && damageable.Status != null)
            {
                damageable.Status.MultiplyAttack(attackMultiplier);
            }
        }
    }
}
