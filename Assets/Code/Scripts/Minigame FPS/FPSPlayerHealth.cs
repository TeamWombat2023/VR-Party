using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FPSPlayerHealth : MonoBehaviour {
    [SerializeField] public int health;
    public bool isLocalPlayer;
    public bool isImmortal = false;

    [PunRPC]
    public void FPSDamageTake(int _damage) {
        if (!isImmortal) {
            health -= _damage;

            if (health <= 0) {
                if (isLocalPlayer)
                    FPSNetworkManager.instance.SpawnPlayersWithDelay();

                Destroy(gameObject);
            }
        }
    }
}