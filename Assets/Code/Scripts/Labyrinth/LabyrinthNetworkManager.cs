using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class LabyrinthNetworkManager : MonoBehaviourPunCallbacks {
    [Space] [SerializeField] private Transform spawnPoint;
    [Space] [SerializeField] public GameObject roomCam;

    private void Start() {
        Debug.Log("JOINED MINIGAME");
        SpawnPlayersWithDelay();
    }

    public void SpawnPlayersWithDelay() {
        PlayerManager.LocalXROrigin.transform.position = spawnPoint.position;
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }
}