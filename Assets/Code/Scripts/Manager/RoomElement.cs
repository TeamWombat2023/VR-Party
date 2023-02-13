using TMPro;
using UnityEngine;
using Photon.Realtime;

public class RoomElement : MonoBehaviour {
    
    [SerializeField]
    private TMP_Text roomInfoText;

    private RoomManager RoomManager { get; set; }
    private RoomInfo RoomInfo { get; set; }

    public void SetRoomInfo(RoomInfo roomInfo) {
        RoomInfo = roomInfo;
        roomInfoText.text = roomInfo.MaxPlayers + " " + roomInfo.Name;
    }
    public void SetRoomManager(RoomManager roomManager) {
        RoomManager = roomManager;
    }
    public void OnClickButton() {
        Debug.Log("OnClick");
        RoomManager.OnClick_RoomElement(RoomInfo);
    }
}