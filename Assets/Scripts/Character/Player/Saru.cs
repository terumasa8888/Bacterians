public class Saru : CharacterBase
{
    protected override void InitializeAttackBehaviour(Status status)
    {
        SetAttackBehaviour(new NormalAttack(status.Attack, gameObject));
        // �Ⴆ�Έȉ��̂悤�ɏ���������ƍU���p�^�[���𔚔��U���ɕύX���邱�Ƃ��ł���
        // SetAttackBehaviour(new ExplosionAttack(status));
    }
}