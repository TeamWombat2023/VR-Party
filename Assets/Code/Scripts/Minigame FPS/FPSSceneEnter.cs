using TMPro;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;

public class FPSSceneEnter : MonoBehaviourPunCallbacks
{
    void OnTriggerEnter(Collider other){
        Debug.Log("asfasfasfsa");
        PhotonNetwork.LoadLevel("Minigame FPS Scene");
    }
}
