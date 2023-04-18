using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks {
    public GameObject localXROrigin;
    public GameObject mainAvatar;
    public GameObject avatarHead;
    public GameObject avatarBody;
    public InputActionAsset inputActionAsset;
    public GameObject[] avatarModelPrefabs;
    public TMP_Text playerNameText;

    private void Start() {
        var scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene is '" + scene.name + "'.");
        if (photonView.IsMine) {
            // This is my player
            localXROrigin.SetActive(true);
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Avatar", out var avatarNumber))
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int)avatarNumber);

            SetLayerRecursively(avatarHead, LayerMask.NameToLayer("LocalAvatarHead"));
            SetLayerRecursively(avatarBody, LayerMask.NameToLayer("LocalAvatarBody"));
            var inputActionManager = localXROrigin.AddComponent<InputActionManager>();
            inputActionManager.actionAssets = new List<InputActionAsset>() { inputActionAsset };
            mainAvatar.AddComponent<AudioListener>();
            foreach (var componentsInChild in mainAvatar.GetComponentsInChildren<Collider>())
                if (componentsInChild.CompareTag("Body") || componentsInChild.CompareTag("Head"))
                    componentsInChild.isTrigger = false;
        }
        else {
            // This is another player
            localXROrigin.SetActive(false);
            SetLayerRecursively(avatarHead, LayerMask.NameToLayer("Default"));
            SetLayerRecursively(avatarBody, LayerMask.NameToLayer("Default"));
        }

        if (playerNameText != null) playerNameText.text = photonView.Owner.NickName;
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
        SetUpAvatar(avatarHolder.leftHand, avatarInputConverter.avatarLeftHand);
        SetUpAvatar(avatarHolder.rightHand, avatarInputConverter.avatarRightHand);
    }

    private static void SetUpAvatar(Transform avatarModelTransform, Transform mainAvatarTransform) {
        avatarModelTransform.SetParent(mainAvatarTransform);
        avatarModelTransform.localPosition = Vector3.zero;
        avatarModelTransform.localRotation = Quaternion.identity;
    }
}