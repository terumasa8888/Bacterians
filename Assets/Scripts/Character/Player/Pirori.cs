public class Pirori : CharacterBase
{
    protected override void Awake()
    {
        base.Awake();
        InitializeAttackBehaviour<ExplosionAttack>();
    }
}