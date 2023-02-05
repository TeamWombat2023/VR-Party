using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class VRKeyboardManager : MonoBehaviour {
    
    public Vector3 relativePosition = new Vector3(0f, 0f, 0.5f);
    public GameObject playerCamera;
    public Keyboard keyboard;
    public List<TMP_InputField> inputFields;
    public float keyboardLerpSpeed = 0.05f;
    [SerializeField] private InputActionReference openCloseKeyboard;
    
    private int _currentInputFieldNumber;
    private GameObject _keyboard;
    private Transform _keyboardTransform;
    private Vector3 _keyboardPosition;
    private Quaternion _playerRotation;
    private Quaternion _keyboardRotation;
    private readonly Quaternion _keyboardRotator = Quaternion.Euler(-45, 0, 0);
    private readonly Quaternion _angleRotator = Quaternion.Euler(20, 0, 0);
    private void Start() {
        _keyboard = keyboard.gameObject;
        _keyboardTransform = _keyboard.transform;
    }

    private void Update() {
        if (!_keyboard.activeSelf) return;
        
        _playerRotation = playerCamera.transform.rotation;
        _keyboardRotation = _keyboardTransform.rotation;
            
        _keyboardPosition = 
            Vector3.Lerp(_keyboardTransform.position, 
                playerCamera.transform.position + _playerRotation * _angleRotator * relativePosition
                , keyboardLerpSpeed);
        _keyboardPosition.y = _keyboardPosition.y < 0 ? 0 : _keyboardPosition.y;

        _keyboardRotation = Quaternion.Lerp(_keyboardRotation,
            _playerRotation * _keyboardRotator, keyboardLerpSpeed);

        _keyboardTransform.position = _keyboardPosition;
        _keyboardTransform.rotation = _keyboardRotation;
    }
    
    public void EnableVRKeyboard(int inputFieldNumber) {
        if ( _keyboard.activeSelf) {
            _keyboard.SetActive(false);
        }
        else {
            _keyboardTransform.position =
                playerCamera.transform.position + _playerRotation * _angleRotator * relativePosition;
        
            _keyboardRotation = _playerRotation * _keyboardRotator;
        }
        
        _keyboard.SetActive(true);

        _currentInputFieldNumber = inputFieldNumber;
        keyboard.inputField = inputFields[_currentInputFieldNumber];
    }

    public void DisableVRKeyboard() {
        keyboard.typingArea.setLeftHandLaser(true);
        keyboard.typingArea.setRightHandLaser(true);
        
        _keyboard.SetActive(false);
    }
    
    private void OnEnable() {
        openCloseKeyboard.action.performed += OpenCloseKeyboard;
    }

    private void OnDisable() {
        openCloseKeyboard.action.performed -= OpenCloseKeyboard;
    }

    private void OpenCloseKeyboard(InputAction.CallbackContext obj) {
        if (_keyboard.activeSelf) {
            DisableVRKeyboard();
        }
        else {
            EnableVRKeyboard(_currentInputFieldNumber);
        }
    }
}