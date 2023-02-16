using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager{get; private set;}
    
    public UnitHealth _playerHealth = new UnitHealth();


    void Awake()
    {
        //make sure there is only one game manager
        if(gameManager != null && gameManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameManager = this;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
