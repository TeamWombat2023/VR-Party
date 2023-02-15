using TMPro;
using UnityEngine;
using Photon.Realtime;

public class RoomElement : MonoBehaviour {
    
    [SerializeField]
    private TMP_Text roomNameText;
    [SerializeField]
    private TMP_Text roomPlayerCountText;
    [SerializeField]
    private TMP_Text roomRegionText;

    private RoomManager RoomManager { get; set; }
    private RoomInfo RoomInfo { get; set; }
    private string RoomRegion { get; set; }

    public void SetRoomInfo(RoomInfo roomInfo) {
        RoomInfo = roomInfo;
        roomNameText.text = roomInfo.Name;
        roomPlayerCountText.text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;
        SetRoomRegion(roomInfo.CustomProperties.ContainsKey("Region") ? roomInfo.CustomProperties["Region"].ToString() : "EU");
        roomRegionText.text = RoomRegion;
    }
    public RoomInfo GetRoomInfo() {
        return RoomInfo;
    }
    public void SetRoomManager(RoomManager roomManager) {
        RoomManager = roomManager;
    }
    private void SetRoomRegion(string roomRegion) {
        RoomRegion = roomRegion;
    }
    public string GetRoomRegion() {
        return RoomRegion;
    }
    
    public void OnClickButton() {
        RoomManager.OnClick_RoomElement(RoomInfo);
    }
}