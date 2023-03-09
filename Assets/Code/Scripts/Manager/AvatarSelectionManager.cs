using Photon.Pun;
using UnityEngine;

public class AvatarSelectionManager : MonoBehaviour {
    private AvatarSelectionManager _instance;
    private int _selectedAvatar = 0;
    private int _currentAvatar = 0;

    public GameObject[] avatars;
    public AvatarInputConverter avatarInputConverter;

    private void Awake() {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    private void Start() {
        LoadAvatar(_currentAvatar);
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
            { { "Avatar", _currentAvatar } });
    }

    public void SelectAvatar(int avatarNumber) {
        _selectedAvatar = avatarNumber;
        Debug.Log("Selected Avatar: " + _selectedAvatar);
        LoadAvatar(_selectedAvatar);
    }

    public void DeselectAvatar() {
        LoadAvatar(_currentAvatar);
    }

    public void SaveAvatar() {
        _currentAvatar = _selectedAvatar;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
            { { "Avatar", _currentAvatar } });
    }

    private void LoadAvatar(int avatarNumber) {
        foreach (var avatar in avatars) avatar.SetActive(false);
        avatars[avatarNumber].SetActive(true);
        avatarInputConverter.mainAvatarTransform = avatars[avatarNumber].GetComponent<AvatarHolder>().mainAvatar;
        avatarInputConverter.avatarBody = avatars[avatarNumber].GetComponent<AvatarHolder>().avatarBody;
        avatarInputConverter.avatarHead = avatars[avatarNumber].GetComponent<AvatarHolder>().avatarHead;
        avatarInputConverter.avatarLeftHand = avatars[avatarNumber].GetComponent<AvatarHolder>().leftHand;
        avatarInputConverter.avatarRightHand = avatars[avatarNumber].GetComponent<AvatarHolder>().rightHand;
    }
}