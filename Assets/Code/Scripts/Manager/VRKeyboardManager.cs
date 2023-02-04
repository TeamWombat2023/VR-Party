using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using Keyboard = VRKeys.Keyboard;

public class VRKeyboardManager : MonoBehaviour {
    public Vector3 relativePosition = new Vector3(0f, 0f, 2f);
    public GameObject playerCamera;
    public Keyboard keyboard;
    public List<TMP_InputField> inputFields;
    public GameObject leftHandController;
    public GameObject rightHandController;
    public GameObject leftMallet;
    public GameObject rightMallet;

    private int _currentInputFieldNumber;

    public void EnableVRKeyboard(int inputFieldNumber) {
        _currentInputFieldNumber = inputFieldNumber;
        keyboard.Enable();
        keyboard.canvas.gameObject.SetActive(false);
        keyboard.OnUpdate.AddListener(HandleUpdate);
        keyboard.OnSubmit.AddListener(HandleSubmit);
        keyboard.OnCancel.AddListener(HandleCancel);

        keyboard.gameObject.transform.position = playerCamera.transform.position + relativePosition;
        AttachMallets();

        leftHandController.GetComponent<XRRayInteractor>().enabled = false;
        rightHandController.GetComponent<XRRayInteractor>().enabled = false;
    }

    private void AttachMallets() {
        leftMallet.transform.SetParent(leftHandController.transform);
        leftMallet.transform.localPosition = Vector3.zero;
        leftMallet.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        leftMallet.SetActive(true);

        rightMallet.transform.SetParent(rightHandController.transform);
        rightMallet.transform.localPosition = Vector3.zero;
        rightMallet.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        rightMallet.SetActive(true);
    }

    private void DetachMallets() {
        leftMallet.transform.SetParent(null);
        leftMallet.SetActive(false);

        rightMallet.transform.SetParent(null);
        rightMallet.SetActive(false);
    }

    private void DisableVRKeyboard() {
        keyboard.OnUpdate.RemoveListener(HandleUpdate);
        keyboard.OnSubmit.RemoveListener(HandleSubmit);
        keyboard.OnCancel.RemoveListener(HandleCancel);

        keyboard.Disable();

        DetachMallets();

        leftHandController.GetComponent<XRRayInteractor>().enabled = true;
        rightHandController.GetComponent<XRRayInteractor>().enabled = true;
    }

    private void HandleUpdate(string text) {
        keyboard.HideValidationMessage();
        var inputField = inputFields[_currentInputFieldNumber];
        inputField.text += text.Substring(text.Length - 1);
        inputField.caretPosition = inputField.text.Length;
    }

    private void HandleSubmit(string text) {
        DisableVRKeyboard();

        var eventSystem = EventSystem.current;
        if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
    }

    private void HandleCancel() {
        DisableVRKeyboard();

        var eventSystem = EventSystem.current;
        if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
    }
}