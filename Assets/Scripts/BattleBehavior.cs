using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackStatus { Idle, Stunned, Attacking, Blocking, Evading, Dead }

public class BattleBehavior : MonoBehaviour
{

    public bool isAttacking;
    public float lastAttackTime;


    public AttackStatus attackStatus;

}
