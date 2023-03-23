using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class FPSNetworkManager : MonoBehaviourPunCallbacks {

    public static FPSNetworkManager instance;

    [SerializeField] private GameObject fpsVRPlayerPrefab;
    [Space]
    [SerializeField] private Transform spawnPoint;
    [Space]
    [SerializeField] public GameObject roomCam;

    void Awake(){
        instance = this;
    }

    private void Start() {
        Debug.Log("JOINED MINIGAME");
        
        roomCam.SetActive(false);
        SpawnPlayerWithDelay();
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


    public void SpawnPlayerWithDelay(){
        roomCam.SetActive(true);
        Invoke("RespawnPlayer", 4);
    }

    public void RespawnPlayer(){
        roomCam.SetActive(false);
        GameObject _player = PhotonNetwork.Instantiate(fpsVRPlayerPrefab.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<FPSPlayerHealth>().isLocalPlayer = true;
    }

}