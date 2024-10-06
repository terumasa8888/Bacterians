using System.Collections;
using UnityEngine;

public class Status : MonoBehaviour {
    [SerializeField] private CharacterData characterData;
    [SerializeField] private ParticleSystem deadEffect;
    [SerializeField] private ParticleSystem healEffect;

    //private ButtonType buttonType;
    private int hp;
    private int attack;
    private float speed;
    private int multiplySpeed;
    private int healPower;
    private int duplicatableNumber;
    private float duplicateInterval;

    private Sprite characterSprite;

    

    void Awake() {
        //buttonType = characterData.ButtonType;
        hp = characterData.Hp;
        attack = characterData.Attack;
        speed = characterData.Speed;
        multiplySpeed = characterData.MultiplySpeed;
        healPower = characterData.HealPower;
        duplicatableNumber = characterData.DuplicatableNumber;
        duplicateInterval = characterData.DuplicateInterval;
        characterSprite = characterData.CharacterSprite;
    }

    public int Attack {
        get { return attack; }
    }

    public int HealPower {
        get { return healPower; }
    }

    public int DuplicatableNumber {
        get { return duplicatableNumber; }
    }

    public float DuplicateInterval {
        get { return duplicateInterval; }
    }

    public void TakeDamage(int amount) {
        hp -= amount;
        if (hp <= 0) {
            Die();
        }
    }

    void Die() {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Heal(int amount) {
        hp += amount;
        Instantiate(healEffect, transform.position, Quaternion.identity);
    }

    public void ReduceDuplicatableNumber() {
        duplicatableNumber--;
    }

}
