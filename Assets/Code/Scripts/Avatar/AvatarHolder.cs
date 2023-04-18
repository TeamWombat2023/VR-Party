using System;
using UnityEngine;

public class AvatarHolder : MonoBehaviour {
    public Transform mainAvatar;
    public Transform avatarHead;
    public Transform avatarBody;
    public Transform leftHand;
    public Transform rightHand;

    private void Start() {
        SetLayerRecursively(avatarHead.gameObject, LayerMask.NameToLayer("LocalAvatarHead"));
        SetLayerRecursively(avatarBody.gameObject, LayerMask.NameToLayer("LocalAvatarBody"));
    }

    private static void SetLayerRecursively(GameObject go, int newLayer) {
        if (null == go) return;
        go.layer = newLayer;
        foreach (Transform child in go.transform) {
            if (null == child) continue;
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision with " + collision.gameObject.name);
    }
}