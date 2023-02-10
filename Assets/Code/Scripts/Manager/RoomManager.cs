using TMPro;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using System.Collections.Generic;

public class RoomManager : MonoBehaviourPunCallbacks {
    
    // Nickname
    [SerializeField]
    private TMP_InputField nicknameInputField;
    
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
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnectedAndReady) {
            PhotonNetwork.ConnectUsingSettings();
        }
        else {
            PhotonNetwork.JoinLobby();
        }
    }
    
    public void DisconnectServer() {
        PhotonNetwork.Disconnect();
    }
    
    private void SetNickname() {
        PhotonNetwork.NickName = nicknameInputField.text == "" ? "Player" + Random.Range(0, 1000) : nicknameInputField.text;
    }

    public void CreateRoom() {
        if (newRoomNameInputField.text == "" || !PhotonNetwork.IsConnected) return;
        SetNickname();
        RoomOptions roomOptions = new RoomOptions {
            MaxPlayers = byte.Parse(maxPlayersDropdown.options[maxPlayersDropdown.value].text),
            IsVisible = isPrivateToggle.isOn
        };
        PhotonNetwork.JoinOrCreateRoom(newRoomNameInputField.text, roomOptions, TypedLobby.Default);
        
    }
    public override void OnCreatedRoom() {
        PhotonNetwork.LoadLevel("Lobby Scene");
    }

    public void JoinRoom() {
        if (joinRoomNameInputField.text == "" || !PhotonNetwork.IsConnected) return;
        PhotonNetwork.JoinRoom(joinRoomNameInputField.text);
        PhotonNetwork.LoadLevel("Lobby Scene");
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
