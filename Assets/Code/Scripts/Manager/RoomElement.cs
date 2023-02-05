using TMPro;
using UnityEngine;
using Photon.Realtime;

public class RoomElement : MonoBehaviour {
    
    [SerializeField]
    private TMP_Text roomInfoText;
    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo) {
        RoomInfo = roomInfo;
        roomInfoText.text = roomInfo.MaxPlayers + " " + roomInfo.Name;
    }    
}
