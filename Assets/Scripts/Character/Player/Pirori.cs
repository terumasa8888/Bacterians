public class Pirori : CharacterBase
{
    protected override void InitializeAttackBehaviour(Status status)
    {
        SetAttackBehaviour(new ExplosionAttack(status));
        // �Ⴆ�Έȉ��̂悤�ɏ���������ƍU���p�^�[����ʏ�U���ɕύX���邱�Ƃ��ł���
        // SetAttackBehaviour(new NormalAttack(status.Attack, gameObject));
    }
}