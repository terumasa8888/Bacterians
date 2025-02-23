public class Pirori : CharacterBase
{
    protected override void InitializeAttackBehaviour(Status status)
    {
        SetAttackBehaviour(new ExplosionAttack(status));
        // 例えば以下のように書き換えると攻撃パターンを通常攻撃に変更することができる
        // SetAttackBehaviour(new NormalAttack(status.Attack, gameObject));
    }
}