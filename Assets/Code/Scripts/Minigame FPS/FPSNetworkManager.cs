using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using System.Collections;

public class FPSNetworkManager : MonoBehaviourPunCallbacks {
    public static FPSNetworkManager instance;

    [SerializeField] private GameObject fpsVRPlayerPrefab;
    [Space] [SerializeField] public GameObject roomCam;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Debug.Log("JOINED MINIGAME");
        SpawnPlayersWithDelay();
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


    public void SpawnPlayersWithDelay() {
        PlayerManager.LocalXROrigin.transform.position = Vector3.zero;
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
        // StartCoroutine(MyCoroutine(_player));
    }


    public void RespawnWithDelay(GameObject _player) {
        StartCoroutine(RespawnPlayer(_player));
    }

    IEnumerator RespawnPlayer(GameObject _player) {
        yield return new WaitForSeconds(3.0f);
        _player.transform.GetChild(0).gameObject.transform.position = Vector3.zero;
        _player.transform.GetChild(0).gameObject.transform.rotation = Quaternion.identity;
        _player.GetComponent<PlayerManager>().health = 100;
        _player.SetActive(true);
        //StartCoroutine(MakePlayerMortal(_player));
    }
    IEnumerator MakePlayerMortal(GameObject _player){
        yield return new WaitForSeconds(5.0f);
        Debug.Log("MORTAL YAPTI");
        _player.GetComponent<PlayerSetup>().OpenWeapon();
        _player.GetComponent<PlayerManager>().isImmortal = false;
    }
    
}