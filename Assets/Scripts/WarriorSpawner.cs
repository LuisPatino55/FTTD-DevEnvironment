using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WarriorSpawner : MonoBehaviour
{
    [SerializeField] private List<string> warriorNamesFemale = new()
    {
    "Athena", "Seraphia", "Isabella", "Aurora", "Freya","Luna", "Supernova", "Nightshade", "Cassia", "Selene", "Celeste", "Quinlan", "Rosalind",
    "Bellatrix", "Lysandra", "Elara", "Serenity", "Marcella", "Astrid", "Lightning","Aria", "Valeria", "Sable", "Galadriel", "Fioretti", "Eowyn",
    "Zenobia", "Morgana", "Andromeda", "Morgaine", "Rowan", "Iliana", "Calista", "Maelstrom","Mirella", "Siren", "Nike", "Odin's Queen", "Agasaya",
    "Skydancer", "Sugarkiss", "Heidi the Beast", "Seraph", "Yennefer", "Siri", "Rogue", "Oaxisha","Amara", "Brynn", "Daphne", "Finley", "Giselle",
    "Kalista", "Lenora", "Marigold", "Ondine", "Paloma","Dara", "Gaia", "Hera", "Inga", "Juno", "Kara", "Maia", "Nia", "Orla", "Petra", "Rhea",
    "Vespera", "Winifred", "Xanthe", "Yara", "Astra", "Blair", "Clio", "Daria", "Echo", "Faye", "Gwen", "Ingrid", "Fafnir's Talon", "Innana", "Thalia",
    "Jade", "Kira", "Lila", "Mara", "Nova", "Ophelia", "Piper", "Quinn", "Raven", "Skye", "Zoe", "Alba", "Bria", "Halcyon", "Juniper", "Nyx",
    "Evadne", "Elowynn", "Sylvestra", "Valentina", "Sapphire", "Phoenix", "Bloodlust","Sela", "Tara", "Zara", "Gwynn"
    };
    [SerializeField] private List<string> warriorNamesMale = new()
    {
    "Steel Death", "Thunderstrike", "Razorback", "Iron Fist", "Inferno Knight", "Midnight Marauder", "Viper's Sting", "Goliath", "Mauler", "Ember Sentinel",
    "The Titan", "Stormbringer", "Havoc", "Tempest Reaper", "Aegis", "The Savage", "Ironclad Killer", "Tyr", "Ares", "Tyler", "Obsidian", "Scorchblade",
    "Vanguard", "Berserker", "Doombringer", "Hailstorm's Hero", "Astral Fury", "Tornado", "Onyx", "Rhino", "Shadowlord","Shadowraith", "Rump Buster",
    "Nighterror", "Noobster", "Chippy Head","Barakka", "Honey Badger",  "Woden","Anhur", "Odin's Spawn", "Nidhogg", "Behemoth", "Huitzilopochtli", "Seth",
    "Alaric", "Beckett", "Caspian", "Dorian", "Finnian", "Grigor", "Jaxon", "Keegan", "Lysander", "Maddox", "Niall", "Osiris","Xipec", "Ullr", "Orson", "Rafe",
    "Percival", "Quillon", "Ragnar", "Silas", "Thaddeus", "Ulysses", "Virgil", "Wolfgang", "Xerxes", "Yannick", "Zephyr", "Uriel", "Vance", "Xander", "Zane",
    "Brutus", "Cato", "Draco", "Einar", "Fergus", "Gaius", "Hector", "Ivar", "Jace", "Kato", "Leon", "Magnus", "Nero", "Orin","Lucian", "The Luis", "Wolfblade",
    "Primo", "Quintus", "Rolf", "Sven", "Titus", "Blaine", "Corbin", "Eamon", "Flint", "Garret", "Hades", "Ivan", "Kane", "Revenant", "Delarno", "Cursed Blade", "Ricky",
    };
    

    private Dictionary<string, bool> WarriorNamesAndGender = new();

    [Space(10)]
    [Header("             ==========  190 Max combined NPC opponents  ============")]
    [SerializeField][Space(20)][Range(0, 50)] private int easyLevelFighters = 0;
    [SerializeField][Space(20)][Range(0, 50)] private int mediumLevelFighters = 0;
    [SerializeField][Space(20)][Range(0, 40)] private int hardLevelFighters = 0;
    [SerializeField][Space(20)][Range(0, 30)] private int eliteLevelFighters = 0;
    [SerializeField][Space(20)][Range(0, 20)] private int beastLevelFighters = 0;

    public List<Warrior> ConfiguredBaseWarriors = new();
    
    private void Awake()
    {
        //Set base names and gender bool to WarriorNamesAndGender Dictionary
        foreach (string name in warriorNamesFemale) { WarriorNamesAndGender.Add(name, true); }  
        foreach (string name in warriorNamesMale) { WarriorNamesAndGender.Add(name, false); }
    }
    private void Start()
    {
        // Debug for process above 
        Debug.Log("Dictionary length:" + WarriorNamesAndGender.Count);
        foreach (KeyValuePair<string, bool> pair in WarriorNamesAndGender)
        { Debug.LogFormat(" Warrior Name: {0} - Female: {1}", pair.Key, pair.Value); }

        SpawnBaseWarriors();
    }
    private void SpawnBaseWarriors()    // Loops through difficulty settings to spawn appropriate enemies
    {
        if (WarriorNamesAndGender.Count != 0)
        {
            if (beastLevelFighters > 0) { SetLevelStats(beastLevelFighters, WarriorDifficulty.Beast, 5); }
            else if (eliteLevelFighters > 0) { SetLevelStats(eliteLevelFighters, WarriorDifficulty.Elite, 4); }
            else if (hardLevelFighters > 0) { SetLevelStats(hardLevelFighters, WarriorDifficulty.Hard, 3); }
            else if (mediumLevelFighters > 0) { SetLevelStats(mediumLevelFighters, WarriorDifficulty.Medium, 2); }
            else if (easyLevelFighters > 0) { SetLevelStats(easyLevelFighters, WarriorDifficulty.Easy, 1); }
            else
            {
                Debug.Log("NPC warrior creation is now complete. Warriors left in dictionary:" + WarriorNamesAndGender.Count);
                SetLevelStats(WarriorNamesAndGender.Count, WarriorDifficulty.Easy, 0);
            }
        }
      //  else { PrintStats(storeManager.warriorsForSale); }
    }

    private void SetLevelStats(int spawnCount, WarriorDifficulty difficulty, int list)
    {
        switch (list)  // set spawn count to 0 for each difficulty variable, so loop works properly
        {
            case 0: Debug.Log("Begin store stock warrior creation"); break;
            case 1: easyLevelFighters = 0; break;
            case 2: mediumLevelFighters = 0; break;
            case 3: hardLevelFighters = 0; break;
            case 4: eliteLevelFighters = 0; break;
            case 5: beastLevelFighters = 0; break;
        }
        for (int i = 0; i < spawnCount; i++) {  // spawn # of warriors set in each difficulty variable

            if (WarriorNamesAndGender.Count == 0) {
                Debug.LogFormat("Error... no entires in WarriorNamesAndGender Dictionary. Spawn Count: {0}  Difficulty: {1}", spawnCount, difficulty);
                return; }
            
            else { // Get random base warrior key & value from dictionary
                int war = Random.Range(0, WarriorNamesAndGender.Count);
                KeyValuePair<string, bool> randomPair = WarriorNamesAndGender.ElementAt(war);
                string warName = randomPair.Key;
                bool warGender = randomPair.Value;
                int warLevel;
                
                // set random warrior level within difficulty parameters
                if (difficulty == WarriorDifficulty.Beast) { warLevel = Random.Range(28, 32); }
                else if (difficulty == WarriorDifficulty.Elite) { warLevel = Random.Range(22, 28); }
                else if (difficulty == WarriorDifficulty.Hard) { warLevel = Random.Range(15, 22); }
                else if (difficulty == WarriorDifficulty.Medium) { warLevel = Random.Range(8, 15); }
                else if (difficulty == WarriorDifficulty.Easy) { warLevel = Random.Range(2, 8); }
                else { warLevel = 1; }
                
                // Spawn warrior & remove entry from dictionary
                Debug.LogFormat("generating warrior: {0}, female: {1}, Level: {2}, Difficulty: {3}  - Dictionary Entries: {4}", 
                warName, warGender, warLevel, difficulty, WarriorNamesAndGender.Count);
                if (list == 0)
                {  // storeManager.warriorsForSale.Add(new Warrior(warName, warGender, warLevel, difficulty));
                }
                else { 
                    ConfiguredBaseWarriors.Add(new Warrior(warName, warGender, warLevel, difficulty));
                }
                WarriorNamesAndGender.Remove(warName);
            }
        }
        Debug.Log(spawnCount + " Warriors created at: " + difficulty + " difficulty level.");
        SpawnBaseWarriors();
    }
    private void PrintStats(List<Warrior> holdingList)
    {
        Debug.Log("Listing available store stock warriors...");
        foreach (Warrior war in holdingList)
        {
        Debug.LogFormat("Warrior: {0}  female= {1}, Combat level: {2}" + "   Difficulty: " + war.warriorDifficulty + "   Health: " + war.currentHealth + "/" + war.maxHealth + "   Stamina: " + war.currentStamina + "/" + war.maxStamina +  " Accuracy: " + war.accuracySkill + "     Evasion: " + war.evasionSkill + " ", war.warriorName, war.isFemale, war.combatLevel);
        }
       // Debug.Log("Warriors vailable in the store: " + storeManager.warriorsForSale.Count);
        Debug.Log("NPC Warriors Available: " + ConfiguredBaseWarriors.Count);
    }
}