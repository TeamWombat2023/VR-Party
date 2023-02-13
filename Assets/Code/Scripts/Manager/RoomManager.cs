using TMPro;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;

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
    [SerializeField]
    private TMP_Dropdown regionDropdownCreateMenu;

    // Join Room
    [SerializeField]
    private TMP_Dropdown regionDropdownJoinPrivateMenu;
    [SerializeField]
    private TMP_InputField joinRoomNameInputField;
    
    // Room List
    [SerializeField] 
    private RoomElement roomElementPrefab;
    [SerializeField] 
    private Transform content;
    [SerializeField]
    private TMP_Dropdown regionDropdownJoinPublicMenu;

    private Dictionary<string, RoomElement> _cachedRoomList = new Dictionary<string, RoomElement>();
    private RoomInfo _selectedRoomInfo;
    
    private void Start() {
        regionDropdownCreateMenu.onValueChanged.AddListener(delegate {
            SetRegion(regionDropdownCreateMenu);
        });
        regionDropdownJoinPrivateMenu.onValueChanged.AddListener(delegate {
            SetRegion(regionDropdownJoinPrivateMenu);
        });
        regionDropdownJoinPublicMenu.onValueChanged.AddListener(delegate {
            SetAndRefreshRegion(regionDropdownJoinPublicMenu);
        });
    }

    public void ConnectServer() {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsConnectedAndReady) return;
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "eu";
        PhotonNetwork.ConnectUsingSettings();
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
        var customRoomProperties = new Hashtable {
            {"Region", regionDropdownCreateMenu.options[regionDropdownCreateMenu.value].text}
        };
        
        var roomOptions = new RoomOptions {
            MaxPlayers = byte.Parse(maxPlayersDropdown.options[maxPlayersDropdown.value].text),
            IsVisible = isPrivateToggle.isOn,
            CustomRoomProperties = customRoomProperties
        };
        
        PhotonNetwork.JoinOrCreateRoom(newRoomNameInputField.text, roomOptions, TypedLobby.Default);
    }

    public void JoinRoomWithName() {
        if (joinRoomNameInputField.text == "" || !PhotonNetwork.IsConnected) return;
        PhotonNetwork.JoinRoom(joinRoomNameInputField.text);
    }
    
    public void OnClick_Join() {
        if (_selectedRoomInfo == null) return;
        PhotonNetwork.JoinRoom(_selectedRoomInfo.Name);
    }
    public void OnClick_RoomElement(RoomInfo roomInfo) {
        _selectedRoomInfo = roomInfo;
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
    public void ClearRoomList() {
        foreach (var roomElement in _cachedRoomList.Values) {
            Destroy(roomElement.gameObject);
        }
        _cachedRoomList.Clear();
    }
    private void SetRegion(TMP_Dropdown region) {
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();
    }
    private void SetAndRefreshRegion(TMP_Dropdown region) {
        SetRegion(region);
        var currentRegion = region.options[region.value].text;
        foreach (var roomElement in _cachedRoomList.Values.Where(roomElement => currentRegion != roomElement.GetRoomRegion())) {
            Destroy(roomElement.gameObject);
            _cachedRoomList.Remove(roomElement.GetRoomInfo().Name);
        }
    }
}