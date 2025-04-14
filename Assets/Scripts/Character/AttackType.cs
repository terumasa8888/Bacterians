using System.Collections.Generic;

public enum AttackType
{
    NormalAttack,   // �ʏ�U��
    ExplosionAttack, // �����U��
    RotatingAttack,  // ��]�U��
    None // �U�����Ȃ�
}

public static class AttackTypeExtensions
{
    private static readonly Dictionary<AttackType, string> DisplayNames = new Dictionary<AttackType, string>
    {
        { AttackType.NormalAttack, "�ʏ�U��" },
        { AttackType.ExplosionAttack, "�����U��" },
        { AttackType.RotatingAttack, "��]�U��" },
        { AttackType.None, "�U�����Ȃ�" }
    };

    public static string GetDisplayName(this AttackType attackType)
    {
        return DisplayNames.TryGetValue(attackType, out var displayName) ? displayName : attackType.ToString();
    }
}
