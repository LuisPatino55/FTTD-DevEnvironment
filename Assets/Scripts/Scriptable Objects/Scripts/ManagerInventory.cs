using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "F_TT_D/Manager Inventory")]
public class ManagerInventory : ScriptableObject
{
    public int inventoryID;
    public string inventoryName;
    [Space(10)]
    public int currencyAmount;
    [Space(10)]
    [Range(1, 100)] public float currentFame = 0;
    [Range(1, 100)] public float currentNotority = 0;
    [Range(1, 5)] public int barracksLevel = 1;
    [Space(10)]
    public List<ArmorItem> armorItems = new();
    [Space(10)]
    public List<WeaponItem> weaponItems = new();
    [Space(10)]
    public List<ShieldItem> shieldItems = new();
    [Space(10)]
    public List<Consumable> consumables = new();
}