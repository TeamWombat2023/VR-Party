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
    public GameObject[] avatarModelPrefabs;

    private void Start() {
        if (photonView.IsMine) {
            // This is my player
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Avatar", out var avatarNumber)) {
                Debug.Log("Avatar Selection Number: " + avatarNumber);
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int)avatarNumber);
            }

            SetLayerRecursively(avatarHead, LayerMask.NameToLayer("LocalAvatarHead"));
            SetLayerRecursively(avatarBody, LayerMask.NameToLayer("LocalAvatarBody"));
            var inputActionManager = localXROrigin.AddComponent<InputActionManager>();
            inputActionManager.actionAssets = new List<InputActionAsset>() { inputActionAsset };
            localXROrigin.SetActive(true);
        }
        else {
            // This is another player
            SetLayerRecursively(avatarHead, LayerMask.NameToLayer("Default"));
            SetLayerRecursively(avatarBody, LayerMask.NameToLayer("Default"));
            localXROrigin.SetActive(false);
        }
    }

    private static void SetLayerRecursively(GameObject obj, int newLayer) {
        if (null == obj) return;
        obj.layer = newLayer;
        foreach (Transform child in obj.transform) {
            if (null == child) continue;
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber) {
        var selectedAvatar = Instantiate(avatarModelPrefabs[avatarSelectionNumber], localXROrigin.transform);

        var avatarInputConverter = localXROrigin.GetComponent<AvatarInputConverter>();
        var avatarHolder = selectedAvatar.GetComponent<AvatarHolder>();
        SetUpAvatar(avatarHolder.avatarHead, avatarInputConverter.avatarHead);
        SetUpAvatar(avatarHolder.avatarBody, avatarInputConverter.avatarBody);
        SetUpAvatar(avatarHolder.leftHand, avatarInputConverter.leftHandController);
        SetUpAvatar(avatarHolder.rightHand, avatarInputConverter.rightHandController);
    }

    private static void SetUpAvatar(Transform avatarModelTransform, Transform mainAvatarTransform) {
        avatarModelTransform.SetParent(mainAvatarTransform);
        avatarModelTransform.localPosition = Vector3.zero;
        avatarModelTransform.localRotation = Quaternion.identity;
    }
}