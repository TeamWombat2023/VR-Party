using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabAnimateHandOnInput : MonoBehaviourPunCallbacks {
    public InputActionProperty grabAnimationAction;

    public Animator handAnimator;

    // Start is called before the first frame update
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
        if (PhotonNetwork.IsConnected && photonView.IsMine) {
            var gripValue = grabAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Grip", gripValue);
        }
    }
}