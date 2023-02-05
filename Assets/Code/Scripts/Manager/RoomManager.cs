using TMPro;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using System.Collections.Generic;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // Create Room
    [SerializeField]
    private TMP_InputField newRoomNameInputField;
    [SerializeField]
    private TMP_Dropdown maxPlayersDropdown;
    [SerializeField]
    private Toggle isPrivateToggle;

    // Join Room
    [SerializeField]
    private TMP_InputField joinRoomNameInputField;
    
    // Room List
    [SerializeField] 
    private RoomElement roomElementPrefab;
    [SerializeField] 
    private Transform content;

    private List<RoomElement> _roomElements;
    
    public void ConnectServer() {
        PhotonNetwork.ConnectUsingSettings();
    }
    

    public void CreateRoom() {
        if (newRoomNameInputField.text == "" || !PhotonNetwork.IsConnected) return;
        RoomOptions roomOptions = new RoomOptions {
            MaxPlayers = byte.Parse(maxPlayersDropdown.options[maxPlayersDropdown.value].text),
            IsVisible = !isPrivateToggle.isOn
        };
        PhotonNetwork.JoinOrCreateRoom(newRoomNameInputField.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.JoinRoom(newRoomNameInputField.text);
        PhotonNetwork.LoadLevel("Lobby Scene");
    }

    public void JoinRoom() {
        if (joinRoomNameInputField.text == "" || !PhotonNetwork.IsConnected) return;
        PhotonNetwork.JoinRoom(joinRoomNameInputField.text);
        PhotonNetwork.LoadLevel("Lobby Scene");
    }
    
    public string GetRoomInfo() {
        Room currentRoom = PhotonNetwork.CurrentRoom;
        return "Room name: " + currentRoom.Name + "\n" +
                             "Players: " + currentRoom.PlayerCount + "/" + currentRoom.MaxPlayers;
    }

    public void DisconnectServer() {
        PhotonNetwork.Disconnect();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach (var room in roomList) {
            if (room.RemovedFromList) {
                var index = _roomElements.FindIndex(x => x.RoomInfo.Name == room.Name);
                if (index == -1) continue;
                Destroy(_roomElements[index].gameObject);
                _roomElements.RemoveAt(index);
            }
            else {
                var roomElement = Instantiate(roomElementPrefab, content);
                if (roomElement == null) continue;
                roomElement.SetRoomInfo(room);
                _roomElements.Add(roomElement);
            }
        }
    }
}
