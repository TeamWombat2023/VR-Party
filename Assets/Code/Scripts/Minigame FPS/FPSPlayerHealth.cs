using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FPSPlayerHealth : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;

    [PunRPC]
    public void FPSDamageTake(int _damage){
        health -= _damage;

        if(health <= 0){

            if(isLocalPlayer)
                FPSNetworkManager.instance.SpawnPlayerWithDelay();

            Destroy(gameObject);
        }
    }
}
