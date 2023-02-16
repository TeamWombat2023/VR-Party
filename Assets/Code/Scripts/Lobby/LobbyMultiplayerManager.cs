using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyMultiplayerManager : MonoBehaviourPunCallbacks
{
    public TMP_Text playerNamesText;
    public TMP_Text lobbyInfoText;
    public TMP_Text debugText;
    
    private List<string> _playerNames = new List<string>();

    public override void OnJoinedRoom() {
        ShowPlayers();
        WriteLobbyInformation(PhotonNetwork.CurrentRoom);
    }

    private void ShowPlayers() {
        var players = "";
        foreach (var player in PhotonNetwork.PlayerList) {
            players += player.NickName + "\n";
        }
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
        lobbyInfoText.text = "Lobby Name: " + room.Name +"\n";
        lobbyInfoText.text += "Players:" + room.PlayerCount + "/"+ room.MaxPlayers+ "\n";
    }
}
