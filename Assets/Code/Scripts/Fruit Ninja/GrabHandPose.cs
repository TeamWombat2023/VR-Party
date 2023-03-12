using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class GrabHandPose : MonoBehaviour {
    public GameObject finalRightHand;
    public GameObject rightHandOrigin;

    private void Start() {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(RemovePose);
        finalRightHand.gameObject.SetActive(false);
    }

    private void SetupPose(BaseInteractionEventArgs args) {
        if (args.interactorObject is XRDirectInteractor) {
            rightHandOrigin.SetActive(false);
            finalRightHand.SetActive(true);
        }
    }

    private void RemovePose(BaseInteractionEventArgs args) {
        if (args.interactorObject is XRDirectInteractor) {
            finalRightHand.SetActive(false);
            rightHandOrigin.SetActive(true);
        }
    }
}