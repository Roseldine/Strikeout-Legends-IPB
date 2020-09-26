using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Strikeout Legends/ Characters/ Character")]
public class SOCharacter : ScriptableObject
{
    [Header("Character Info")]
    public new string name;
    [TextArea(10,5)]
    public string descrition;

    public int health;
    public int energy;
    public float speed;

    [Header("Attacks")]
    public PlayerAttackController.specialSpawnPlace specialSpawnPlace;
    public int basicDamage;
    public int specialDamage;
    public float basicSpeed;
    public float specialSpeed;

    [Header("Objects")]
    public GameObject characterModel;
    public GameObject basicAttack;
    public GameObject specialAttack;
    public GameObject dashTrail;

    [Tooltip("Character icon and spell icons, 0-passive, 1-basic, 2-special")]
    public Sprite[] icons;

    [TextArea(7,5)]
    [Tooltip("Spell descriptions, 0-passive, 1-basic, 2-special")]
    public string[] spellDescriptions;

    [Tooltip("Array that holds all character skins")]
    public SOCharacterSkin[] skins;
}
