using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviourPunCallbacks {
    [SerializeField] private GameObject GenericVRPlayerPrefab;

    public Vector3 spawnPosition;

    // private void Start() {
    //     Invoke(nameof(SpawnPlayer), 0.5f);
    // }
    //
    // private void SpawnPlayer() {
    //     PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
    // }

    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
    }
    
    
    
}