using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class FPSNetworkManager : MonoBehaviourPunCallbacks {

    [SerializeField] private GameObject fpsVRPlayerPrefab;
    [Space]
    [SerializeField] private Transform spawnPoint;

    public override void OnJoinedRoom() {
        PhotonNetwork.Instantiate(fpsVRPlayerPrefab.name, spawnPoint.position, Quaternion.identity);
        //lobbyCanvas.worldCamera = fpsVRPlayerPrefab.GetComponentInChildren<Camera>();
        //lobbyInfoCanvas.worldCamera = fpsVRPlayerPrefab.GetComponentInChildren<Camera>();
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