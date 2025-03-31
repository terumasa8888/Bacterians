using System.Collections;
using UnityEngine;
using UniRx;

/// <summary>
/// ��]���Ȃ���A�G�ꂽ�G�Ƀ_���[�W��^����U��
/// </summary>
public class RotatingAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private float minAttackDuration = 3f; // �U�����鎞�Ԃ̍ŏ��l
    [SerializeField] private float maxAttackDuration = 5f; // �U�����鎞�Ԃ̍ő�l
    [SerializeField] private float minWaitTime =5f; // �U���̃C���^�[�o���̍ŏ��l
    [SerializeField] private float maxWaitTime = 7f; // �U���̃C���^�[�o���̍ő�l
    [SerializeField] private float detectionRadius = 3f; // �U���Ώۂ����o���锼�a

    [SerializeField] private GameObject attackEffect; // �U���̃G�t�F�N�g
    [SerializeField] private GameObject attackSound; // �U����

    private bool isAttacking = false;
    private float attackDuration;
    private float waitTime;

    private CharacterBase character;

    private void Start()
    {
        // �����_���ȉ�]���ԂƃC���^�[�o��������
        attackDuration = Random.Range(minAttackDuration, maxAttackDuration);
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        // CharacterBase�̃C���X�^���X���擾
        character = GetComponent<CharacterBase>();

        // ��莞�Ԃ��Ƃ�isAttacking��؂�ւ���
        StartCoroutine(RotationRoutine());
    }

    private void Update()
    {
        if (isAttacking)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public void Attack(IDamageable attacker, IDamageable target)
    {
        if (!isAttacking) return;

        // �U���̏���
        target.TakeDamageFrom(attacker);

        // �U���G�t�F�N�g�Ɖ��̍Đ�
        if (attackEffect != null)
        {
            // Instantiate(attackEffect, attacker.GameObject.transform.position, Quaternion.identity);
        }
        if (attackSound != null)
        {
            /*var soundPlayer = GetComponentInParent<CharacterBase>().soundPlayer;
            soundPlayer.PlaySound(attackSound);*/
        }
    }

    /// <summary>
    /// ��莞�Ԃ��Ƃɉ�]��؂�ւ���
    /// </summary>
    private IEnumerator RotationRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (ShouldRotate())
            {
                character.Rotator.PauseRotation();
                isAttacking = true;
                yield return new WaitForSeconds(attackDuration);
                isAttacking = false;
                character.Rotator.ResumeRotation();
            }
        }
    }

    /// <summary>
    /// �U���ł���I�u�W�F�N�g���߂��ɂ��邩�ǂ����𔻒肷��
    /// </summary>
    /// <returns>�����𖞂����I�u�W�F�N�g������ꍇ��true�A����ȊO��false</returns>
    private bool ShouldRotate()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var obj in nearbyObjects)
        {
            if (obj.gameObject.CompareTag(gameObject.tag)) continue;

            IDamageable target = obj.GetComponent<IDamageable>();
            if (target == null) continue;
            return true;
        }
        return false;
    }
}


