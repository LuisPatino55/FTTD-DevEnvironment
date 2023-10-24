using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DevScreenUI : PrintUI
{
    public RectTransform LeftSideListPanel;
    public GameObject LeftPanelSelector;
    public GameObject SpawnAndSaveSelector;
    public TextMeshProUGUI MainTextBox;

    private void Awake()
    {
        LeftSideListPanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LeftSideListPanel.gameObject.SetActive(false);
            LeftPanelSelector.SetActive(true);
            SpawnAndSaveSelector.SetActive(true);
            MainTextBox.text = "";
        }
    }

    public override void PrintStats(List<Warrior> holdingList)                               // prints full roster on list 
    {
        MainTextBox.text = "Listing available warriors in: <color=blue>" + holdingList.ToString() + "</color>.\n";
        foreach (Warrior war in holdingList) { StatsToTextBox(war); }
        Debug.Log("Warriors Available: " + holdingList.Count);
    }

    public override void PrintStats(List<Warrior> holdingList, WarriorDifficulty difficulty) // prints roster filtered by difficulty
    {
        int i = 0;
        MainTextBox.text = "Listing available <color=red>" + difficulty + "</color> warriors in: <color=blue>" + holdingList + "</color>.\n";
        foreach (Warrior war in holdingList)
        { if (war.warriorDifficulty == difficulty) { StatsToTextBox(war); } }   // filter out warriors that don't match the difficulty
        Debug.Log("Warriors Available: " + i);
    }

    private void StatsToTextBox(Warrior war)                                    // Prints warrior stats to the main text box
    {
        MainTextBox.text += "Warrior: <color=red>" + war.WarriorName + "</color>   Combat level: " + war.combatLevel + "  Difficulty: " + war.warriorDifficulty + "  Health: " + war.maxHealth + "  Stamina: "
        + war.maxStamina + "  Accuracy: " + war.accuracySkill + "  Evasion: " + war.evasionSkill + "  ID: <color=red>" + war.WarriorID + "</color>  Female: " + war.IsFemale + "\n";
    }

    public void PrintOpponentList(int difficulty)                              // used for the left panel difficulty selector
    {
        WarriorDifficulty Difficulty = (WarriorDifficulty)difficulty;
        Debug.Log("Printing opponet warrior roster. Difficulty: " + Difficulty);
        PrintStats(DataManager.Instance.ConfiguredOpponentWarriors, Difficulty);
    }

}
