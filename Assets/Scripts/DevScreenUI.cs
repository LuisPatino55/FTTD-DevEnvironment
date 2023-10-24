using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevScreenUI : PrintUI
{
    public RectTransform LeftSideListPanel;
    public GameObject LeftPanelSelector;
    public GameObject SpawnAndSaveSelector;

    private void Awake()
    {
        LeftSideListPanel.gameObject.SetActive(false);

    }

    public override void PrintStats(List<Warrior> holdingList)                               // prints full roster on list 
    {
        Debug.Log("Listing available warriors in: <color:blue>" + holdingList + "</color>.");
        foreach (Warrior war in holdingList)
        {
            Debug.LogFormat("Warrior: {0}  female= {1}, Combat level: {2}" + "   Difficulty: " + war.warriorDifficulty + "   Health: " + war.maxHealth + "/"
            + war.maxStamina + " Accuracy: " + war.accuracySkill + "     Evasion: " + war.evasionSkill + "  ID: {3}", war.WarriorName, war.IsFemale, war.combatLevel, war.WarriorID);
        }
        Debug.Log("Warriors Available: " + holdingList.Count);
    }

    public override void PrintStats(List<Warrior> holdingList, WarriorDifficulty difficulty) // filters out selected difficulty and prints roster
    {
        int i = 0;
        Debug.Log("Listing available <color:red>" + difficulty + "</color> warriors in: <color:blue>" + holdingList + "</color>.");
        foreach (Warrior war in holdingList)
        {
            if (war.warriorDifficulty == difficulty)
            {
                Debug.LogFormat("Warrior: {0}  female= {1}, Combat level: {2}" + "   Difficulty: " + war.warriorDifficulty + "   Health: " + war.maxHealth + "/"
                + war.maxStamina + " Accuracy: " + war.accuracySkill + "     Evasion: " + war.evasionSkill + "  ID: {3}", war.WarriorName, war.IsFemale, war.combatLevel, war.WarriorID);
                i++;
            }
        }
        Debug.Log("Warriors Available: " + i);
    }

    public void PrintOpponentList(int difficulty)                                   // used for the UI difficulty selector
    {
        WarriorDifficulty Difficulty = (WarriorDifficulty)difficulty;
        Debug.Log("Printing opponet warrior roster. Difficulty: " + Difficulty);
        PrintStats(DataManager.Instance.ConfiguredOpponentWarriors, Difficulty);
    }

}
