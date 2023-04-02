using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlaneGameNetworkManager : MonoBehaviourPunCallbacks
{
   [SerializeField] private GameObject planePrefab;
    [Space]
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform[] spawnPoints;
    
    private void Start() {
        Debug.Log("JOINED MINIGAME");
        GameObject _player = PhotonNetwork.Instantiate(planePrefab.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        PhotonNetwork.LoadLevel("Lobby Scene");
    }

    public override void OnLeftRoom() {
        PhotonNetwork.Disconnect();
    }
    
    public override void OnDisconnected(DisconnectCause cause) {
        PhotonNetwork.LoadLevel("Lobby Scene");
    }
}
