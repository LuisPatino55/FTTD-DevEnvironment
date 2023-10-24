using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public enum WarriorList { Player, Opponent, Store, Dead }

    [Serializable]
    public struct BattleHistory
    {
        public string OpponentName;
        public int OpponentID;
        public int OpponentLevel;
        public bool Win;
    }

    public static DataManager Instance;
    [Space(10)]
    public List<Warrior> ConfiguredOpponentWarriors = new();
    [Space(10)]
    public List<Warrior> StoreInventoryWarriors = new();
    [Space(10)]
    public List<Warrior> PlayerBarracks = new();
    [Space(10)]
    public List<Warrior> DeadWarriors = new();
    [Space(10)]
    public Warrior[] ArenaWarriors = new Warrior[2];

    // key will be the warrior ID, value will be a list of battle history
    public Dictionary<int, List<BattleHistory>> battleHistory = new();


    private void Awake()
    {
        if(Instance != null) { Destroy(gameObject); return; }               // Singleton pattern
        Instance = this; DontDestroyOnLoad(gameObject);
    }

    public void AddWarriorToList(Warrior warrior, WarriorList list)         //add warrior to appropriate list
    {
        switch (list)
        {
            case WarriorList.Opponent:
                ConfiguredOpponentWarriors.Add(warrior);
                break;
            case WarriorList.Store:
                StoreInventoryWarriors.Add(warrior);
                break;
            case WarriorList.Player:
                PlayerBarracks.Add(warrior);
                break;
            case WarriorList.Dead:
                DeadWarriors.Add(warrior);
                break;
            default:
                Debug.Log("Error: WarriorList not found");
                break;
        }

    }
    public void HistoryEnrty(int id, Warrior opponent)
    {
        BattleHistory historyEntry = new();
        historyEntry.OpponentName = opponent.WarriorName;
        historyEntry.OpponentID = opponent.WarriorID;
        historyEntry.OpponentLevel = opponent.combatLevel;
        historyEntry.Win = true;
        battleHistory[id].Add(historyEntry);
    }


}

