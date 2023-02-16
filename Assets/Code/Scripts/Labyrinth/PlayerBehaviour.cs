using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerBehaviour : MonoBehaviour
{
    void Start()
    {

    }
    
    void Update()
    {
        
    }

    private void PlayerTakeDmg(int damage)
    {
        GameManager.gameManager._playerHealth.TakeDamage(damage);
        Debug.Log(GameManager.gameManager._playerHealth.currentHealth);
    }

    private void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.Heal(healing);
        Debug.Log(GameManager.gameManager._playerHealth.currentHealth);
    }

    void OnTriggerEnter(Collider other) 
    {
        //if heal object
        if (other.gameObject.CompareTag ("HealPickup"))
        {
            PlayerHeal(10);
        }
        //if damage object
        else if (other.gameObject.CompareTag ("DamagePickup"))
        {
            PlayerTakeDmg(10);
        }
        //if score object

        //destroy after pickup
        other.gameObject.SetActive (false);
        
    }
}