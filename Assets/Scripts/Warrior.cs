using System;
using System.Collections.Generic;
using UnityEngine;

public enum WarriorDifficulty { Beast = 5, Elite = 4, Hard = 3, Medium = 2, Easy = 1 }
public enum WarriorAttackStatus { Idle, Stunned, Attacking, Blocking, Evading, Dead }
public class Warrior
{
    public bool isFemale = false;
    public bool isAttacking;
    public string warriorName = "";
    public string warriorBio = "";

    public int experiencePoints = 0;
    [Range(100, 10000)] public int expToNextLevel = 100;
    [Range(1, 30)] public int combatLevel;

    [SerializeField][Range(100, 1000)] public int maxHealth = 100;
    public int currentHealth;

    [Range(10, 100)] public float maxStamina = 20; // needed to attack or block, but not to evade
    public float currentStamina;

    [Range(1, 25)] public int strength = 1; // affects weapon base damage 1 = *0.1  10 = *1  25 = *2.5

    [Range(1, 25)] public int vitality = 1; // affects damage received 1 = *2.5  10 = *1  25 = *0.1

    [Range(1, 25)] public int dexterity = 1; // affects critical hit chance 1 = *0.1  10 = *1  25 = *2.5

    [Range(1, 25)] public int agility = 1; // affects chance to evade attack 1 = *0.1  10 = *1  25 = *2.5

    [Range(1, 100)] public float accuracySkill = 1; // governs chance of landing hits, duh ;) 

    [Range(1, 100)] public float evasionSkill = 1;  // governs chance of opposing attacks missing this warrior

    [Range(1, 100)] public float blockingSkill = 1; // chance to block partial or full damage

    public float maxPanicPool;                               // panic level will affect attack cooldown, blocking, accuracy, evasion, and raise chance of stun 
    public float currentPanic = 0;

    public int costToBuy;
    public int costToSell;

    public float lastAttackTime;
    public bool isRegening = false;
    public bool isBleeding = false;

    public WarriorDifficulty warriorDifficulty;             // Enums for diffiuclty and attacks
    public WarriorAttackStatus attackStatus;

    [Range(1, 100)] public float headDurability = 100;      // Body part durability or amputations
    public bool hasRightEye = true;
    public bool hasLeftEye = true;
    [Range(1, 100)] public float torsoDurability = 100;
    [Range(1, 100)] public float leftArmDurability = 100;
    public bool hasLeftArm = true;
    [Range(1, 100)] public float rightArmDurability = 100;
    public bool hasRightArm = true;
    [Range(1, 100)] public float leftLegDurability = 100;
    public bool hasLeftLeg = true;
    [Range(1, 100)] public float rightLegDurability = 100;
    public bool hasRightLeg = true;

    [Range(-10, 10)] public float ownerBond = 0;            // will give bonus + or - to healing body parts
    [Range(-10, 10)] public float ownerRespect = 0;         // will give bonus + or - to learning and stamina
    [Range(-10, 10)] public float ownerAttraction = 0;      

    public ArmorItem armorItemSlot = null;
    public WeaponItem weaponItemSlot = null;
    public WeaponItem shieldItemSlot = null;
    public ProfilePic ProfilePic = null;

    public Dictionary<string, int> battleHistory;

    // ===================================================================  Class constructor
    public Warrior(string warriorName = "Generic Bro", bool isFemale = false, int combatLevel = 1, WarriorDifficulty difficulty = WarriorDifficulty.Easy, string warriorBio = "Unknown History")
    {
        battleHistory = new Dictionary<string, int>();
        this.warriorName = warriorName;
        this.warriorBio = warriorBio;
        this.combatLevel = combatLevel;
        this.isFemale = isFemale;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        warriorDifficulty = difficulty;
        accuracySkill = UnityEngine.Random.Range(combatLevel * 2, combatLevel * 3);  //set Accuracy & evasion
        evasionSkill = UnityEngine.Random.Range(combatLevel * 2, combatLevel * 3);
        
        for (int i = 0; i < (combatLevel * 3); i++)                                       // set initial STR - AGI stats
        { switch (UnityEngine.Random.Range(0, 4))
            { case 0: strength++; break; case 1: vitality++; break; case 2: dexterity++; break; case 3: agility++; break; }
        }

        Debug.LogFormat("New warrior spawned: {0}  female= {1}, Level: {2}", this.warriorName, this.isFemale, this.combatLevel);
        GainLevelEnd();
    }

    // ========================================================================== Level Up
    public void GainLevel()
    {
        combatLevel++;

        if (combatLevel <= 34)
        {
            for (int i = 0; i < 3; i++)
            { 
                switch (UnityEngine.Random.Range(0, 4)) { 
                    case 0: strength++; break; 
                    case 1: vitality++; break; 
                    case 2: dexterity++; break; 
                    case 3: agility++; break; }
            }
        } else { return; }
        Debug.LogFormat("{0} gained a level! Level: {1}", warriorName, combatLevel);
        GainLevelEnd();
    }
    private void GainLevelEnd()
    {
        maxPanicPool = 30 + (combatLevel * 2);
        maxHealth = (combatLevel * 15) + 100;
        maxStamina = (combatLevel * 3) + 20;
        currentHealth = maxHealth;
        currentStamina = maxStamina;

        switch (combatLevel)
        {
            case 1: warriorDifficulty = WarriorDifficulty.Easy; expToNextLevel = 100; costToBuy = combatLevel * 50; break;
            case 2: case 3: case 4: case 5: expToNextLevel = 100 * combatLevel; costToBuy = combatLevel * 50; break;
            case 6: case 7: expToNextLevel = 125 * combatLevel; costToBuy = combatLevel * 75; break;
            case 8: expToNextLevel = 125 * combatLevel; warriorDifficulty = WarriorDifficulty.Medium; costToBuy = combatLevel * 75; break;
            case 9: case 10: expToNextLevel = 125 * combatLevel; costToBuy = combatLevel * 90; break;
            case 11: case 12: case 13: case 14: expToNextLevel = 150 * combatLevel; costToBuy = combatLevel * 100; break;
            case 15: expToNextLevel = 150 * combatLevel; warriorDifficulty = WarriorDifficulty.Hard; costToBuy = combatLevel * 100; break;
            case 16: case 17: case 18: case 19: expToNextLevel = 200 * combatLevel; costToBuy = combatLevel * 125; break;
            case 20: case 21: expToNextLevel = 200 * combatLevel; costToBuy = combatLevel * 150; break;
            case 22: expToNextLevel = 200 * combatLevel; warriorDifficulty = WarriorDifficulty.Elite; costToBuy = combatLevel * 150; break;
            case 23: case 24: case 25: expToNextLevel = 250 * combatLevel; costToBuy = combatLevel * 175; break;
            case 26: case 27: expToNextLevel = 300 * combatLevel; costToBuy = combatLevel * 200; break;
            case 28: expToNextLevel = 300 * combatLevel; warriorDifficulty = WarriorDifficulty.Beast; costToBuy = combatLevel * 250; break;
            case 29: case 30: expToNextLevel = 300 * combatLevel; costToBuy = combatLevel * 300; break;
            case 31: case 32: expToNextLevel = 400 * combatLevel; costToBuy = combatLevel * 400; break;
        }
        if (this.combatLevel >= 32) { expToNextLevel = 400 * combatLevel; }
        Debug.LogFormat("{0}  female= {1}, Level: {2}  Difficulty: {3}  Health: {4}/{5}  Stamina: {6}/{7}  Accuracy: {8}  Evasion: {9}  STR: {10}  VIT: {11}  DEX: {12}  AGI: {13}  Cost to buy: {14} Next lvl: {15}",
        warriorName, isFemale, combatLevel, warriorDifficulty, currentHealth, maxHealth, currentStamina, maxStamina, accuracySkill, evasionSkill, strength, vitality, dexterity, agility, costToBuy, expToNextLevel);
    }




    // ======================================================================================================================================== HP loss and gain
    public void TakeDamage(int damageAmount)
    {
        if (currentPanic < maxPanicPool) { currentPanic = Mathf.Min(currentPanic += (damageAmount / 2), maxPanicPool); } // add 1/2 of dmg taken to the panic pool
        currentHealth = Mathf.Max(currentHealth - damageAmount, 0); if (currentHealth <= 0) { Die(); }                  // take damage and death on 0 hp
    }

    public void BleedOut(int damageAmount, int statusDuration)                                                          // applies DOT hp reduction based on (damage amount per sec) (effect duration)
    {
        isBleeding = true;
        for (float i = 0f; i < statusDuration; i += 1 * Time.deltaTime) {
            if (currentPanic < maxPanicPool){ currentPanic++; }                                                         // adds 1 point panic per sec of DOT effect
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
            if (currentHealth <= 0) { i = statusDuration; Die(); } }
        isBleeding = false;
    }
    public void GainHealth(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }
    public void RegenHealth(int healAmount, int statusDuration)
    {
        isRegening = true;
        for (float i = 0f; i < statusDuration; i += 1 * Time.deltaTime)
        {
            currentPanic--;
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
            if (currentHealth >= maxHealth)
            {
                i = statusDuration;
            }
        }
        isRegening = false;
    }

    //====================================================================================== Death 
    private void Die()
    {
        Debug.Log(warriorName + "is dead.");
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
}