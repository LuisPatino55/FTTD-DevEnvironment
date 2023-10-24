using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrintUI : MonoBehaviour
{
    public abstract void PrintStats(List<Warrior> holdingList);                               // prints full roster on list argument
    public abstract void PrintStats(List<Warrior> holdingList, WarriorDifficulty difficulty); // filters out selected difficulty and prints list argument
    

}
