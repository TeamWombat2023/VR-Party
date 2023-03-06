using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks {
    public TMP_Text playerNamesText;
    public TMP_Text lobbyInfoText;
    public GameObject startTheGameButton;

    [SerializeField] private GameObject xrOrigin;
    [SerializeField] private GameObject genericVRPlayerPrefab;
    [SerializeField] private Vector3 spawnPosition;
    public Canvas lobbyCanvas;
    public Canvas lobbyInfoCanvas;

    public override void OnJoinedRoom() {
        ShowPlayers();
        WriteLobbyInformation(PhotonNetwork.CurrentRoom);
        startTheGameButton.SetActive(PhotonNetwork.IsMasterClient);
        xrOrigin.SetActive(false);
        PhotonNetwork.Instantiate(genericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
        lobbyCanvas.worldCamera = genericVRPlayerPrefab.GetComponentInChildren<Camera>();
        lobbyInfoCanvas.worldCamera = genericVRPlayerPrefab.GetComponentInChildren<Camera>();
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        PhotonNetwork.LoadLevel("Login Scene");
    }

    public override void OnLeftRoom() {
        PhotonNetwork.Disconnect();
    }
    
    public override void OnDisconnected(DisconnectCause cause) {
        PhotonNetwork.LoadLevel("Login Scene");
    }

    private void ShowPlayers() {
        var players = "";
        foreach (var player in PhotonNetwork.PlayerList) players += player.NickName + "\n";
        playerNamesText.text = players;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        ShowPlayers();
        WriteLobbyInformation(PhotonNetwork.CurrentRoom);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        ShowPlayers();
        WriteLobbyInformation(PhotonNetwork.CurrentRoom);
    }

    private void WriteLobbyInformation(Room room) {
        lobbyInfoText.text = "Lobby Name: " + room.Name + "\n";
        lobbyInfoText.text += "Players:" + room.PlayerCount + "/" + room.MaxPlayers + "\n";
    }

    public override void OnMasterClientSwitched(Player newMasterClient) {
        startTheGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
}