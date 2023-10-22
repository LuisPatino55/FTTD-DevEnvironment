using UnityEngine;

[CreateAssetMenu(menuName = "F_TT_D/Items/Consumable")]
public class Consumable : InventoryItem

{
    public float effectDuration = 0;
    public int raiseHP;
    public int raiseStamina;
    [Space(20)]
    [Header("Status Effects")]
    public bool regenStamina = false;
    public bool mendBody = false;

}