using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks {
    
    public GameObject localXROrigin;
    public GameObject avatarHead;
    public GameObject avatarBody;
    public InputActionAsset inputActionAsset;
    private void Start() {
        if(photonView.IsMine) {
            // This is my player
            SetLayerRecursively(avatarHead, LayerMask.NameToLayer("LocalAvatarHead"));
            SetLayerRecursively(avatarBody, LayerMask.NameToLayer("LocalAvatarBody"));
            var inputActionManager = localXROrigin.AddComponent<InputActionManager>();
            inputActionManager.actionAssets = new List<InputActionAsset>() { inputActionAsset };
            localXROrigin.SetActive(true);
        } else {
            // This is another player
            SetLayerRecursively(avatarHead, LayerMask.NameToLayer("Default"));
            SetLayerRecursively(avatarBody, LayerMask.NameToLayer("Default"));
            localXROrigin.SetActive(false);
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
