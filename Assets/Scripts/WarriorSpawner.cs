using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WarriorSpawner : MonoBehaviour
{
    [Serializable]
    public struct SpawnGroup
    {
        public WarriorDifficulty SpawnDifficulty;
        public bool Females;
        public int SpawnNumber;
        public int IdPrefix;
    }
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private List<string> warriorNamesFemale = new()
    {
    "Athena", "Seraphia", "Isabella", "Aurora", "Freya","Luna", "Supernova", "Nightshade", "Cassia", "Selene", "Celeste", "Quinlan", "Rosalind",
    "Bellatrix", "Lysandra", "Elara", "Serenity", "Marcella", "Astrid", "Lightning","Aria", "Valeria", "Sable", "Galadriel", "Fioretti", "Eowyn",
    "Zenobia", "Morgana", "Andromeda", "Morgaine", "Rowan", "Iliana", "Calista", "Maelstrom","Mirella", "Siren", "Nike", "Odin's Queen", "Agasaya",
    "Skydancer", "Sugarkiss", "Heidi the Beast", "Seraph", "Yennefer", "Siri", "Rogue", "Oaxisha","Amara", "Brynn", "Daphne", "Finley", "Giselle",
    "Kalista", "Lenora", "Marigold", "Ondine", "Paloma","Dara", "Gaia", "Hera", "Inga", "Juno", "Kara", "Maia", "Nia", "Orla", "Petra", "Rhea",
    "Vespera", "Winifred", "Xanthe", "Yara", "Astra", "Blair", "Clio", "Daria", "Echo", "Faye", "Gwen", "Ingrid", "Fafnir's Talon", "Innana", "Thalia",
    "Jade", "Kira", "Lila", "Mara", "Nova", "Ophelia", "Piper", "Quinn", "Raven", "Skye", "Zoe", "Alba", "Bria", "Halcene", "Juniper", "Nyx",
    "Evadne", "Elowynn", "Sylvestra", "Valentina", "Sapphire", "Phoenix", "Bloodlust","Sela", "Tara", "Zara", "Gwynn", "Delila", "Alexandria", "Grim Destiny"
    };
    [Space(10)]
    [SerializeField] private List<string> warriorNamesMale = new()
    {
    "Steel Death", "Thunderstrike", "Razorback", "Iron Fist", "Inferno Knight", "Midnight Marauder", "Viper's Sting", "Goliath", "Mauler", "Ember Sentinel",
    "The Titan", "Stormbringer", "Havoc", "Tempest Reaper", "Aegis", "The Savage", "Ironclad Killer", "Tyr", "Ares", "Tyler", "Obsidian", "Scorchblade",
    "Vanguard", "Berserker", "Doombringer", "Rawdog", "Astral Fury", "Tornado", "Onyx", "Rhino", "Shadowlord","Shadowraith", "Rump Buster",
    "Nighterror", "Alexander", "Chippy Head","Barakka", "Honey Badger", "Woden","Anhur", "Odin's Spawn", "Nidhogg", "Behemoth", "Huitzilopochtli", "Seth",
    "Alaric", "Beckett", "Caspian", "Dorian", "Finnian", "Grigor", "Jaxon", "Keegan", "Lysander", "Maddox", "Niall", "Osiris","Xipec", "Ullr", "Orson", "Rafe",
    "Percival", "Quillon", "Ragnar", "Silas", "Thaddeus", "Ulysses", "Virgil", "Wolfgang", "Xerxes", "Yannick", "Zephyr", "Uriel", "Vance", "Xander", "Zane",
    "Brutus", "Cato", "Draco", "Einar", "Fergus", "Gaius", "Hector", "Ivar", "Jace", "Kato", "Leon", "Magnus", "Nero", "Orin","Lucian", "Aparisio", "Wolfblade",
    "Primo", "Quintus", "Rolf", "Sven", "Titus", "Blaine", "Corbin", "Eamon", "Flint", "Garret", "Hades", "Ivan", "Kane", "Revenant", "Delarno", "Cursed Blade", "Ricky",
    "Xotlopo", "Zeeka", "The Nameless", "Furious George", 
    };
    [Space(10)]
    public SpawnGroup[] OpponentNPCGroup;
    [Space(10)]
    public SpawnGroup[] StoreInventoryGroup;
    private int warriorID = 100;
    
    public DevScreenUI devScreenUI;
    private void Awake()
    {
        Debug.Log("Warrior Spawner initialized");
    }

    public void SpawnOpponents()
    {
        SpawnWarriorGroup(OpponentNPCGroup, DataManager.Instance.ConfiguredOpponentWarriors);
    }
    public void SpawnPlayers()
    {
        SpawnWarriorGroup(StoreInventoryGroup, DataManager.Instance.StoreInventoryWarriors);
    }

    public void SpawnWarriorGroup(SpawnGroup[] spawnGroup, List<Warrior> holdingList)
    {
        Debug.Log("Spawning warrior group" + spawnGroup + "...");
        foreach (SpawnGroup group in spawnGroup)            // loops through all the predefined spawn structs
        {
            Debug.LogFormat("Begin spawing {0} {1} difficulty opponents. Female: {2}", group.SpawnNumber, group.SpawnDifficulty, group.Females);
            for (int i = 0; i < group.SpawnNumber; i++)     // iterates through spawn number for each struct group
            {
                int setID = (group.IdPrefix * 1000) + warriorID;
                SpawnWarrior(setID, group.Females, group.SpawnDifficulty, holdingList);
                warriorID++;
            }
        }
        Debug.LogFormat( spawnGroup + " warrior creation is now complete. Male names free: {0}     Female names free: {1}", warriorNamesMale.Count, warriorNamesFemale.Count);
        devScreenUI.PrintStats(holdingList);
        loadingScreen.SetActive(false);
    }

    public void SpawnWarrior(int id, bool female, WarriorDifficulty difficulty, List<Warrior> holdingList)
    {
        string name;        // will hold random name we grab from names list
        int index;          // will hold index of name we grab
        List<string> names; // holds name of list (either male or female)
        int warLevel;       // will hold random warrior level

        if (female) { names = warriorNamesFemale; }
        else { names = warriorNamesMale; }
        
        if (names.Count <= 0) // if no names left on list, return out
        { Debug.LogFormat("Error... no available names in: {0}", names); return; }
        
        index = Random.Range(0, names.Count - 1);   // random name from list
        name = names[index];           // sets name to spawn
        names.RemoveAt(index);         // removes name from names list
        Debug.Log("Name chosen & removed: " + name + " from: " + names);

        // set random warrior level within difficulty parameters
        if (difficulty == WarriorDifficulty.Beast) { warLevel = Random.Range(28, 32); }
        else if (difficulty == WarriorDifficulty.Elite) { warLevel = Random.Range(22, 28); }
        else if (difficulty == WarriorDifficulty.Hard) { warLevel = Random.Range(15, 22); }
        else if (difficulty == WarriorDifficulty.Medium) { warLevel = Random.Range(8, 15); }
        else if (difficulty == WarriorDifficulty.Easy) { warLevel = Random.Range(2, 8); }
        else { warLevel = 1; }

        //Spawn warrior with all parameters above and add to the configured list 
        holdingList.Add(new Warrior(id, name, female, warLevel, difficulty));
        Debug.LogFormat("generating warrior: {0}, female: {1}, Level: {2}, Difficulty: {3}  ID: {4}  To list: {5}", name, female, warLevel, difficulty, id, holdingList);
    }
}