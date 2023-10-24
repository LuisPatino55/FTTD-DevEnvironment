using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using static DataManager;

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

    public DevScreenUI devScreenUI;

    // key will be the warrior ID, value will be a list of battle history
    public Dictionary<int, List<BattleHistory>> battleHistory = new();

    // XML file paths
    private string _warriorDataPath;
    // XML file names
    private string _xmlOpponentWarriors;
    private string _xmlStoreWarriors;
    private string _xmlBarracksWarriors;
    private string _xmlDeadWarriors;

    // XML dictionary
    private Dictionary<string, List<Warrior>> WarriorXMLs = new();

    private void Awake()
    {
        InitializeXMLs();

        if (Instance != null) { Destroy(gameObject); return; }      // Singleton pattern
        Instance = this; DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (Directory.Exists(_warriorDataPath)) LoadWarriorData();
        else NewDirectory();
    }

    public void AddWarriorToList(Warrior warrior, WarriorList list) //add warrior to appropriate list
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

    public void HistoryEnrty(int id, Warrior opponent, bool win)    // add battle history entry
    {
        BattleHistory historyEntry = new()
        {
            OpponentName = opponent.WarriorName,
            OpponentID = opponent.WarriorID,
            OpponentLevel = opponent.combatLevel,
            Win = win
        };
        battleHistory[id].Add(historyEntry);
    }

    // ====================================================================================================== XML

    private void InitializeXMLs()                                   // initialize XML file paths
    {
        _warriorDataPath = Application.persistentDataPath + "/Warrior_Data/";
        _xmlOpponentWarriors = _warriorDataPath + "OpponentWarriors.xml";
        _xmlStoreWarriors = _warriorDataPath + "StoreWarriors.xml";
        _xmlBarracksWarriors = _warriorDataPath + "BarracksWarriors.xml";
        _xmlDeadWarriors = _warriorDataPath + "DeadWarriors.xml";
        Debug.Log(_warriorDataPath);
        WarriorXMLs.Add(_xmlOpponentWarriors, ConfiguredOpponentWarriors);
        WarriorXMLs.Add(_xmlStoreWarriors, StoreInventoryWarriors);
        WarriorXMLs.Add(_xmlBarracksWarriors, PlayerBarracks);
        WarriorXMLs.Add(_xmlDeadWarriors, DeadWarriors);
        Debug.Log("XMLs initialized");
    }

    private void NewDirectory()                                     // creates save directory
    {
        Directory.CreateDirectory(_warriorDataPath);
        Debug.Log("New save directory created!");
        LoadWarriorData();
    }

    public void LoadWarriorData()                                  // load warrior data from XML files or creates new if they don't exist
    {
        foreach (KeyValuePair<string, List<Warrior>> pair in WarriorXMLs)
        {
            string file = pair.Key;
            List<Warrior> warriorList = pair.Value;
            
            if (File.Exists(pair.Key)) { DeserializeWarriorXML(file, warriorList); }
            else { SerializeWarriorXML(file, warriorList); }
        }
    }
    
    public void SaveWarriorData()                                   // deletes old XML files and saves new ones
    {
        foreach (KeyValuePair<string, List<Warrior>> pair in WarriorXMLs)
        {
            string file = pair.Key;
            List<Warrior> warriorList = pair.Value;
            if (File.Exists(file)) File.Delete(file);
            SerializeWarriorXML(file, warriorList);
        }
    }
    private void SerializeWarriorXML(string filename, List<Warrior> warriorList)      
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Warrior>));
        using FileStream stream = File.Create(filename);
        xmlSerializer.Serialize(stream, warriorList);
        Debug.Log( warriorList + " Warrior data saved!");
    }
    private void DeserializeWarriorXML(string file, List<Warrior> warriorList) 
    {
        if (File.Exists(file))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Warrior>));
            using FileStream stream = File.OpenRead(file);
            var Warriors = (List<Warrior>)xmlSerializer.Deserialize(stream);
            foreach (var warrior in Warriors)
            {
                warriorList.Add(warrior);
            }
            Debug.Log(warriorList + " Warrior data loaded!");
        }
    }
}

