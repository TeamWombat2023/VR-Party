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

    private void IncreaseScore(int score)
    {
        GameManager.gameManager._playerScore.IncreaseScore(score);
        Debug.Log(GameManager.gameManager._playerScore.currentScore);
    }

    private void DecreaseScore(int score)
    {
        GameManager.gameManager._playerScore.DecreaseScore(score);
        Debug.Log(GameManager.gameManager._playerScore.currentScore);
    }

    //actions on collision
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
        else if (other.gameObject.CompareTag ("ScorePickup"))
        {
            IncreaseScore(10);
        }

        //if trap object
        else if (other.gameObject.CompareTag ("TrapPickup"))
        {
            DecreaseScore(10);
        }

        //destroy after pickup
        other.gameObject.SetActive (false);
        
    }
}