using Photon.Pun;
using UnityEngine;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks {
    
    public GameObject localXROrigin;
    public GameObject avatarHead;
    public GameObject avatarBody;
    private void Start() {
        if(photonView.IsMine) {
            // This is my player
            localXROrigin.SetActive(true);
            SetLayerRecursively(avatarHead, LayerMask.NameToLayer("LocalAvatarHead"));
            SetLayerRecursively(avatarBody, LayerMask.NameToLayer("LocalAvatarBody"));
        } else {
            // This is another player
            localXROrigin.SetActive(false);
            SetLayerRecursively(avatarHead, LayerMask.NameToLayer("Default"));
            SetLayerRecursively(avatarBody, LayerMask.NameToLayer("Default"));
        }
    }
    private void SetLayerRecursively(GameObject obj, int newLayer) {
        if (null == obj) {
            return;
        }
        obj.layer = newLayer;
        foreach (Transform child in obj.transform) {
            if (null == child) {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
