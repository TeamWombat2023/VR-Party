using TMPro;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _roomNameInputField;
    
    public void CreateRoom()
    {
        if (_roomNameInputField.text != "" && PhotonNetwork.IsConnected)
        {
            Debug.Log("Create room with name: " + _roomNameInputField.text);
        }
    }
}
