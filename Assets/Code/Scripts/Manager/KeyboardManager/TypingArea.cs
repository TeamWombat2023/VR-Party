using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TypingArea : MonoBehaviour {
    public GameObject leftHandController;
    public GameObject rightHandController;

    public GameObject leftTypingSphere;
    public GameObject rightTypingSphere;

    private XRRayInteractor _leftHandInteractor;
    private XRRayInteractor _rightHandInteractor;

    private void Start() {
        _leftHandInteractor = leftHandController.GetComponent<XRRayInteractor>();
        _rightHandInteractor = rightHandController.GetComponent<XRRayInteractor>();
    }

    private void OnTriggerEnter(Collider other) {

        if (leftHandController == other.gameObject) {
            SetLeftHandLaser(false);
        }
        else if (rightHandController == other.gameObject) {
            SetRightHandLaser(false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (leftHandController == other.gameObject) {
            SetLeftHandLaser(true);
        }
        else if (rightHandController == other.gameObject) {
            SetRightHandLaser(true);
        }
    }

    public void SetLeftHandLaser(bool state) {
        _leftHandInteractor.enabled = state;
        leftTypingSphere.gameObject.SetActive(!state);
    }

    public void SetRightHandLaser(bool state) {
        _rightHandInteractor.enabled = state;
        rightTypingSphere.gameObject.SetActive(!state);
    }
}
