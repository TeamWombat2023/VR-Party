using System;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class GrabHandPose : MonoBehaviour {
    public GameObject rightHandPose;
    public GameObject rightHand;

    private void Start() {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(RemovePose);
        rightHandPose.gameObject.SetActive(false);
    }

    private void SetupPose(BaseInteractionEventArgs args) {
        if (args.interactorObject is XRDirectInteractor) {
            rightHand.SetActive(false);
            rightHandPose.SetActive(true);
        }
    }

    private void RemovePose(BaseInteractionEventArgs args) {
        if (args.interactorObject is XRDirectInteractor) {
            rightHandPose.SetActive(false);
            rightHand.SetActive(true);
        }
    }
}