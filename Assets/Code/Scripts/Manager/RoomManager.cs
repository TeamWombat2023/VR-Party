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

    private Dictionary<string, RoomElement> _cachedRoomList = new Dictionary<string, RoomElement>();
    private RoomInfo _selectedRoomInfo;
    public void ConnectServer() {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnectedAndReady) {
            PhotonNetwork.ConnectUsingSettings();
        }
        else {
            PhotonNetwork.JoinLobby();
        }
    }
    
    public override void OnConnectedToMaster() {
        if (!PhotonNetwork.InLobby) {
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

    public void JoinRoomWithName() {
        if (joinRoomNameInputField.text == "" || !PhotonNetwork.IsConnected) return;
        PhotonNetwork.JoinRoom(joinRoomNameInputField.text);
    }
    
    public void OnClickJoinRoom() {
        if (_selectedRoomInfo == null) return;
        PhotonNetwork.JoinRoom(_selectedRoomInfo.Name);
    }
    public void OnClickRoomElement(RoomInfo roomInfo) {
        _selectedRoomInfo = roomInfo;
        Debug.Log("Selected Room: " + roomInfo.Name);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("Lobby Scene");
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        base.OnRoomListUpdate(roomList);
        UpdateCachedRoomList(roomList);
    }
    
    private void UpdateCachedRoomList(List<RoomInfo> roomList) {
        foreach (var roomInfo in roomList) {
            switch (_cachedRoomList.Count) {
                case > 0 when roomInfo.RemovedFromList:
                    Destroy(_cachedRoomList[roomInfo.Name].gameObject);
                    _cachedRoomList.Remove(roomInfo.Name);
                    break;
                case > 0 when _cachedRoomList.ContainsKey(roomInfo.Name):
                    _cachedRoomList[roomInfo.Name].SetRoomInfo(roomInfo);
                    break;
                default: {
                    var roomElement = Instantiate(roomElementPrefab, content);
                    if (roomElement == null) continue;
                    roomElement.SetRoomInfo(roomInfo);
                    roomElement.SetRoomManager(this);
                    _cachedRoomList.Add(roomInfo.Name, roomElement);
                    break;
                }
            }
        }
    }
}