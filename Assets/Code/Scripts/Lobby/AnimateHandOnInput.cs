using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviourPunCallbacks {
    public InputActionProperty pinchAnimationAction;

    public Animator handAnimator;

    // Start is called before the first frame update
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
        if (PhotonNetwork.IsConnected && photonView.IsMine) {
            var triggerValue = pinchAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Trigger", triggerValue);
        }
    }
}