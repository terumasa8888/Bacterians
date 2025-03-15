public class Saru : CharacterBase
{
    protected override void Awake()
    {
        base.Awake();
        InitializeAttackBehaviour<NormalAttack>();
    }
}
