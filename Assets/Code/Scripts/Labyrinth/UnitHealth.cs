using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitHealth
{
    public int maxHealth = 100;
    public int currentHealth;

    //constructor
    public UnitHealth()
    {
        currentHealth = maxHealth;
    }

    //damage function
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player is dead");
        }
    }

    //healing function
    public void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

}

