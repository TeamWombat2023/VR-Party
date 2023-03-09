using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerHealth : MonoBehaviour
{
    public int health;

    //[PunRPC]
    public void FPSDamageTake(int _damage){
        health -= _damage;

        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
