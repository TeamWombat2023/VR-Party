using TMPro;
using UnityEngine;
using Photon.Realtime;

public class RoomElement : MonoBehaviour {
    
    [SerializeField]
    private TMP_Text roomInfoText;
  
    private RoomInfo RoomInfo { get; set; }

    public void SetRoomInfo(RoomInfo roomInfo) {
        RoomInfo = roomInfo;
        roomInfoText.text = roomInfo.MaxPlayers + " " + roomInfo.Name;
    }    
}