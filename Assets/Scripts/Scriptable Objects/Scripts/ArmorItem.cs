using UnityEngine;

[CreateAssetMenu(menuName = "F_TT_D/Items/ArmorItem")]
public class ArmorItem : InventoryItem
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

}
