using UnityEngine;

public class ItemDestroyedMessage
{
    public string AttackerTag { get; }

    public ItemDestroyedMessage(string attackerTag)
    {
        this.AttackerTag = attackerTag;
    }
}
