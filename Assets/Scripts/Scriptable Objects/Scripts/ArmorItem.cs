using UnityEngine;

[CreateAssetMenu(menuName = "F_TT_D/Items/ArmorItem")]
public class ArmorItem : InventoryItem, IDurable
{
    public enum ArmorType
    {
        noArmor,
        lightArmor,
        mediumArmor,
        heavyArmor
    }
    [Space(20)]
    public ArmorType armorType;

    // [SerializeField][Range(100, 1000)] private maxDurability = 100;
    public float CurrentDurability { get; set; }
    public float MaxDurability { get ; set; }

    public void ReduceDurability()
    {
        
    }

    public void RepairDurability()
    {
        
    }
}
