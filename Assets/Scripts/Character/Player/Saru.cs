public class Saru : CharacterBase
{
    protected override void InitializeAttackBehaviour(Status status)
    {
        SetAttackBehaviour(new NormalAttack(status.Attack, gameObject));
        // 例えば以下のように書き換えると攻撃パターンを爆発攻撃に変更することができる
        // SetAttackBehaviour(new ExplosionAttack(status));
    }
}