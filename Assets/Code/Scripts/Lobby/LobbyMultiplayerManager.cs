using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyMultiplayerManager : MonoBehaviourPunCallbacks
{
    public TMP_Text playerNamesText;
    public TMP_Text lobbyInfoText;

    public TMP_Text debugText;


    private Room room;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        debugText.text = "OnLobby is worked.";
        WriteLobbyInformation();
        Debug.Log("Joined the Lobby.");
    }

    public override void OnJoinedRoom()
    {
        room = PhotonNetwork.CurrentRoom;
        debugText.text = "OnJoinedRoom is worked.";
        WriteLobbyInformation();
        AddUsernamesToList();
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        debugText.text = "OnJoinedRoom failed. "+returnCode+" "+ message;
        Debug.Log("Join the room failed.");
    }
    private void WriteLobbyInformation()
    {
        lobbyInfoText.text = "Lobby Name: " + room.Name +"\n";
        lobbyInfoText.text += "Players:" + room.PlayerCount + "/"+ room.MaxPlayers+ "\n";
    }

    private void AddUsernamesToList()
    {
        foreach (var player in room.Players)
        {
            playerNamesText.text += player.ToString() + "\n";
        }

        playerNamesText.text += "completed\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
