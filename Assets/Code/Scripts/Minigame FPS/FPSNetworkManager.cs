using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using System.Collections;

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
        Invoke("RespawnPlayer", 5);
    }

    public void RespawnPlayer(){
        roomCam.SetActive(false);
        GameObject _player = PhotonNetwork.Instantiate(fpsVRPlayerPrefab.name, spawnPoint.position, Quaternion.identity);
        // StartCoroutine(MyCoroutine(_player));

    }

    // public void MakePlayerMortal(GameObject _player){
    //     Debug.Log("MORTAL YAPTI");
    //     _player.GetComponent<PlayerSetup>().OpenWeapon();
    //     _player.GetComponent<FPSPlayerHealth>().isImmortal = false;
    // }
    //
    // IEnumerator MyCoroutine(GameObject _player)
    // {
    //     yield return new WaitForSeconds(5.0f);
    //     MakePlayerMortal(_player);
    // }

}