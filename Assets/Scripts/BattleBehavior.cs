using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackStatus { Idle, Stunned, Attacking, Blocking, Evading, Dead }

public class BattleBehavior : MonoBehaviour, IBattleable
{

    public bool isAttacking;
    public float lastAttackTime;
    public bool isRegening = false;
    public bool isBleeding = false;

    public AttackStatus attackStatus;

    public int CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int CurrentStamina { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int CurrentPanic { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int MaxPanic { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    //  maxPanicPool = 30 + (combatLevel* 2);

    // ======================================================================================================================================== HP loss and gain
    public void TakeDamage(int damageAmount)
    {
      //  if (currentPanic < maxPanicPool) { currentPanic = Mathf.Min(currentPanic += (damageAmount / 2), maxPanicPool); } // add 1/2 of dmg taken to the panic pool
      //  currentHealth = Mathf.Max(currentHealth - damageAmount, 0); if (currentHealth <= 0) { Die(); }                  // take damage and death on 0 hp
    }

    public void BleedOut(int damageAmount, int statusDuration)                                                          // applies DOT hp reduction based on (damage amount per sec) (effect duration)
    { /*
        isBleeding = true;
        for (float i = 0f; i < statusDuration; i += 1 * Time.deltaTime)
        {
            if (currentPanic < maxPanicPool) { currentPanic++; }                                                         // adds 1 point panic per sec of DOT effect
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
            if (currentHealth <= 0) { i = statusDuration; Die(); }
        }
        isBleeding = false;  */
    }
    public void GainHealth(int healAmount)
    {
     //   currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }
    public void RegenHealth(int healAmount, int statusDuration)
    {  /*
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
        isRegening = false;   */
    }

}
