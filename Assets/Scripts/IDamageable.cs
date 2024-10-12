public interface IDamageable
{
    /// <summary>
    /// ダメージを受けるメソッド
    /// </summary>
    /// <param name="amount">受けるダメージの量</param>
    /// <param name="attackerTag">攻撃者のタグ</param>
    void TakeDamage(int amount, string attackerTag);
}
