using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Keyboard = VRKeys.Keyboard;

public class VRKeyboardManager : MonoBehaviour
{
    public Vector3 relativePosition = new Vector3(0f, 0f, 0.5f);
    public GameObject playerCamera;
    public Keyboard keyboard;
    public TMP_InputField inputField;
    public GameObject leftHandController;
    public GameObject rightHandController;
    public GameObject leftMallet;
    public GameObject rightMallet;
    
    [SerializeField]
    InputActionReference openKeyboardInput;

    private void OnEnable()
    {
        openKeyboardInput.action.performed += EnableKeyboard;
    }

    void EnableKeyboard(InputAction.CallbackContext _)
    {
        keyboard.Enable();
        keyboard.OnUpdate.AddListener(HandleUpdate);
        keyboard.OnSubmit.AddListener(HandleSubmit);
        keyboard.OnCancel.AddListener(HandleCancel);
        
        keyboard.gameObject.transform.position = playerCamera.transform.position + relativePosition;
        AttachMallets();
        
        leftHandController.GetComponent<XRRayInteractor>().enabled = false;
        rightHandController.GetComponent<XRRayInteractor>().enabled = false;
    }

    void DisableKeyboard() 
    {
        keyboard.OnUpdate.RemoveListener(HandleUpdate);
        keyboard.OnSubmit.RemoveListener(HandleSubmit);
        keyboard.OnCancel.RemoveListener(HandleCancel);

        keyboard.Disable();
        DetachMallets();
        
        leftHandController.GetComponent<XRRayInteractor>().enabled = true;
        rightHandController.GetComponent<XRRayInteractor>().enabled = true;
    }
    void HandleUpdate(string newText)
    {
        keyboard.HideValidationMessage();
        inputField.text = newText;
        inputField.caretPosition = newText.Length;
    }
    void HandleSubmit(string newText)
    {
        inputField.text = newText;
        inputField.caretPosition = newText.Length;
        DisableKeyboard();
    }
    void HandleCancel()
    {
        DisableKeyboard();
    }

    void AttachMallets()
    {
        leftMallet.transform.SetParent(leftHandController.transform);
        leftMallet.transform.localPosition = Vector3.zero;
        leftMallet.transform.localRotation = Quaternion.Euler(new Vector3(90f,0f,0f));
        leftMallet.SetActive(true);

        rightMallet.transform.SetParent(rightHandController.transform);
        rightMallet.transform.localPosition = Vector3.zero;
        rightMallet.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        rightMallet.SetActive(true);
    }
    
    void DetachMallets()
    {
        leftMallet.transform.SetParent(null);
        leftMallet.SetActive(false);

        rightMallet.transform.SetParent(null);
        rightMallet.SetActive(false);
    }
}
