using UnityEngine;

/// <summary>
/// �{�X�L�����N�^�[�̃N���X
/// </summary>
public class Boss : CharacterBase
{
    protected override void Awake()
    {
        base.Awake();
        // �{�X�̓��ʂȏ���������������΂����ɋL�q
    }

    private void Update()
    {
        // �{�X�̓��ʂȍU�����W�b�N�������ɋL�q
        PerformSpecialAttack();
    }

    private void PerformSpecialAttack()
    {
        // �{�X�̓��ʂȍU�����W�b�N������
        // ��: ��莞�Ԃ��Ƃɒe�𔭎˂���
    }
}



