using UnityEngine;

public enum SpecialShieldAttack
{
    none,
    shieldBash,
    aegis,
    regen,
}

[CreateAssetMenu(menuName = "F_TT_D/Items/ShieldItem")]
public class ShieldItem : InventoryItem, IDurable
{
    [Range(10f, 100f)] public float shieldAbilityCooldown = 20f;
    [Range(0, 120)] public float abilityDuration = 0f;
    [Range(1, 1000)] public int armorRating = 3;
    [Space(20)]
    public SpecialShieldAttack shieldAttack;

    public float CurrentDurability { get; set; }
    public float MaxDurability { get; set; }

    public void ReduceDurability()
    {

    }

    public void RepairDurability()
    {

    }
    public (int armor, SpecialShieldAttack ability, float cooldown, float duration) ShieldArmorRating(int vitality, int strength)
    {
        float duration = abilityDuration;           // ability buff duration (if any)
        SpecialShieldAttack ability = shieldAttack;  // set shield ability
        float cooldown = Mathf.Max(shieldAbilityCooldown - ((vitality + vitalityBonus + strengthBonus + strength) / 10), 1); // calculate ability cooldown
                  
        int armor = ((vitality + vitalityBonus + 1) / 2) + armorRating + defenseBonus;  // max defense for blocked attacks (full block)

        return (armor, ability, cooldown, duration);
    }
}

