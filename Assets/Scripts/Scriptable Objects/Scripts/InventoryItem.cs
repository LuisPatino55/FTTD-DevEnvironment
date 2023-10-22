using UnityEngine;

public class InventoryItem : ScriptableObject
{
    public string itemName = "";
    public int itemPrice = 0;
    [Space(20)]
    [SerializeField] protected int defenseBonus = 0;
    [SerializeField] protected int attackBonus = 0;
    [Space(10)]
    [Header("Stat Boost")]
    [Range(0, 25)] public float accuracySkill = 0; // governs chance of landing hits, duh ;) 
    [Range(0, 25)] public float evasionSkill = 0;  // governs chance of opposing attacks missing this warrior
    [Range(0, 25)] public float blockingSkill = 0; // chance to block partial or full damage
    [Range(0, 25)] public float unarmedSkill = 0; // bonus to base damage and accuracy for weapon type
    [Range(0, 25)] public float oneHandedSkill = 0; // bonus to base damage and accuracy for weapon type
    [Range(0, 25)] public float twoHandedSkill = 0; // bonus to base damage and accuracy for weapon type
    [Space(10)]
    [SerializeField][Range(0, 10)] protected int strengthBonus = 0; // affects weapon base damage 1 = *0.1  10 = *1  25 = *2.5
    [SerializeField][Range(0, 10)] protected int vitalityBonus = 0; // affects damage received 1 = *2.5  10 = *1  25 = *0.1
    [SerializeField][Range(0, 10)] protected int dexterityBonus = 0; // affects critical hit chance 1 = *0.1  10 = *1  25 = *2.5
    [SerializeField][Range(0, 10)] protected int agilityBonus = 0; // affects chance to evade attack 1 = *0.1  10 = *1  25 = *2.
}