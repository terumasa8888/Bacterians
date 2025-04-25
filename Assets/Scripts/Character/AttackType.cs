using System.Collections.Generic;

public enum AttackType
{
    NormalAttack,   // ’ÊíUŒ‚
    ExplosionAttack, // ”š”­UŒ‚
    RotatingAttack,  // ‰ñ“]UŒ‚
    None // UŒ‚‚µ‚È‚¢
}

public static class AttackTypeExtensions
{
    private static readonly Dictionary<AttackType, string> DisplayNames = new Dictionary<AttackType, string>
    {
        { AttackType.NormalAttack, "’ÊíUŒ‚" },
        { AttackType.ExplosionAttack, "”š”­UŒ‚" },
        { AttackType.RotatingAttack, "‰ñ“]UŒ‚" },
        { AttackType.None, "UŒ‚‚µ‚È‚¢" }
    };

    public static string GetDisplayName(this AttackType attackType)
    {
        return DisplayNames.TryGetValue(attackType, out var displayName) ? displayName : attackType.ToString();
    }
}
