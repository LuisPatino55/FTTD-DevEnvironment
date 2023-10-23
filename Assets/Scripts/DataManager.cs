using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    [Space(10)]
    public List<Warrior> ConfiguredOpponentWarriors = new();
    [Space(10)]
    public Dictionary<string, int> battleHistory = new();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }


    // ======================================================================================== Battle History 
    public void AddToBattleHistory(string key, int value)   // Battle History addition 
    {
        battleHistory[key] = value;
    }

    public int GetValueForBattleHistory(string key)
    {
        if (battleHistory.TryGetValue(key, out int value))
        {
            return value;
        }
        else
        {
            return -1; // or any other appropriate default value
        }
    }
    public void PrintBattleHistory()        // Print the Battle History to the console
    {
        foreach (var kvp in battleHistory)
        {
            Debug.Log("Key: " + kvp.Key + ", Value: " + kvp.Value);
        }
    }
    public void PrintStats(List<Warrior> holdingList)
    {
        Debug.Log("Listing available warriors in:" + holdingList);
        foreach (Warrior war in holdingList)
        {
            Debug.LogFormat("Warrior: {0}  female= {1}, Combat level: {2}" + "   Difficulty: " + war.warriorDifficulty + "   Health: " + war.maxHealth + "/" + war.maxStamina + " Accuracy: " + war.accuracySkill + "     Evasion: " + war.evasionSkill + "  ID: {3}", war.WarriorName, war.IsFemale, war.combatLevel, war.WarriorID);
        }
        // Debug.Log("Warriors vailable in the store: " + storeManager.warriorsForSale.Count);
        Debug.Log("Opponet Warriors Available: " + ConfiguredOpponentWarriors.Count);
    }
}
