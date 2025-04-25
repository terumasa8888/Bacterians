using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private int hp;
    [SerializeField] private int attack;
    [SerializeField] private float speed;
    [SerializeField] private int multiplySpeed;
    [SerializeField] private int healPower;
    [SerializeField] private int duplicatableNumber;
    [SerializeField] private float duplicateInterval;
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private AttackType attackType;

    public ButtonType ButtonType => buttonType;
    public int Hp => hp;
    public int Attack => attack;
    public float Speed => speed;
    public int MultiplySpeed => multiplySpeed;
    public int HealPower => healPower;
    public int DuplicatableNumber => duplicatableNumber;
    public float DuplicateInterval => duplicateInterval;
    public Sprite CharacterSprite => characterSprite;

    // AttackType プロパティに set アクセサを追加
    public AttackType AttackType
    {
        get => attackType;
        set => attackType = value;
    }
}
