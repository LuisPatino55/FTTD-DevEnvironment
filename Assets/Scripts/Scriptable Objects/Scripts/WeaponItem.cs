using UnityEngine;

public enum WeaponDamageType
{
    slashing,
    piercing,
    blunt
}
public enum SpecialWeaponAttack
{
    none,
    knockback,
    berzerk,
    battleStance,
    finesse,
    parry,
}
[CreateAssetMenu(menuName = "F_TT_D/Items/WeaponItem")]

public class WeaponItem : InventoryItem
{
    [Range(0.1f, 10f)] public float attackCooldown = 1;
    [Range(1, 5)] public int criticalMultiplier = 1;
    [Range(1, 1000)] public int baseDamage = 3;
    public bool is2Handed;
    [Space(20)]
    public WeaponDamageType damageType;
    public SpecialWeaponAttack specialAttack;
    public WarriorDifficulty difficultyRequirement;
}
