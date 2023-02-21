using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class VRKeyboardManager : MonoBehaviour {
    
    [SerializeField] private Vector3 relativePosition = new (0f, 0f, 0.5f);
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Keyboard keyboard;
    [SerializeField] private List<TMP_InputField> inputFields;
    [SerializeField] private float keyboardLerpSpeed = 0.05f;
    [SerializeField] private InputActionReference openCloseKeyboard;
    
    private int _currentInputFieldNumber;
    private GameObject _keyboard;
    private readonly Quaternion _keyboardRotator = Quaternion.Euler(-45, 0, 0);
    private readonly Quaternion _angleRotator = Quaternion.Euler(20, 0, 0);
    private void Start() {
        _keyboard = keyboard.gameObject;
        _currentInputFieldNumber = -1;
    }

    private void Update() {
        if (!IsKeyboardActive()) return;
        
        var keyboardPosition = _keyboard.transform.position; 
        keyboardPosition = Vector3.Lerp(keyboardPosition, playerCamera.transform.position + playerCamera.transform.rotation * _angleRotator * relativePosition, keyboardLerpSpeed);
        keyboardPosition.y = keyboardPosition.y < 0 ? 0 : keyboardPosition.y;
        
        _keyboard.transform.position = keyboardPosition;
        _keyboard.transform.rotation = Quaternion.Lerp(_keyboard.transform.rotation,playerCamera.transform.rotation * _keyboardRotator, keyboardLerpSpeed);
    }
    
    public void EnableVRKeyboard(int inputFieldNumber) {
        if (IsKeyboardActive()) {
            _keyboard.SetActive(false);
        }
        else {
            _keyboard.transform.position = playerCamera.transform.position + playerCamera.transform.rotation * _angleRotator * relativePosition;
            _keyboard.transform.rotation = playerCamera.transform.rotation * _keyboardRotator;
        }
        
        _keyboard.SetActive(true);

        _currentInputFieldNumber = inputFieldNumber;
        keyboard.inputField = inputFields[_currentInputFieldNumber];
    }
    
    private void OpenKeyboard() {
        if (keyboard.inputField == null) return;
        _keyboard.transform.position = playerCamera.transform.position + playerCamera.transform.rotation * _angleRotator * relativePosition;
        _keyboard.transform.rotation = playerCamera.transform.rotation * _keyboardRotator;
        _keyboard.SetActive(true);
        keyboard.inputField = inputFields[_currentInputFieldNumber];
    }

    public void DisableVRKeyboard() {
        keyboard.typingArea.SetLeftHandLaser(true);
        keyboard.typingArea.SetRightHandLaser(true);
        _keyboard.SetActive(false);
    }
    
    public bool IsKeyboardActive() {
        return _keyboard.activeSelf;
    }
    
    public void EmptyKeyboardInputField() {
        keyboard.inputField = null;
    }
    
    private void OnEnable() {
        openCloseKeyboard.action.performed += OpenCloseKeyboard;
    }

    private void OnDisable() {
        openCloseKeyboard.action.performed -= OpenCloseKeyboard;
    }

    private void OpenCloseKeyboard(InputAction.CallbackContext obj) {
        if (IsKeyboardActive()) {
            DisableVRKeyboard();
        }
        else {
            OpenKeyboard();
        }
    }
}