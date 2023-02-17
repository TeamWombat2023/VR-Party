using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour {

    public List<GameObject> panels;

    [SerializeField] private InputActionReference openCloseInput;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float menuLerpSpeed = 0.05f;
    [SerializeField] private float menuDistance = 2f;
    [SerializeField] private VRKeyboardManager keyboardManager;
    [SerializeField] private GameObject avatarSelectionPlatform;
    
    private int _currentPanelNumber;
    private bool _isMenuActive;

    void Start() {
        mainMenuPanel.SetActive(_isMenuActive);
        SetupPanels();
    }

    private void Update() {
        if (!_isMenuActive || keyboardManager.IsKeyboardActive()) return;
        
        var mainMenuPosition = mainMenuPanel.transform.position;
        mainMenuPosition = Vector3.Lerp(mainMenuPosition, playerCamera.transform.position + playerCamera.transform.forward * menuDistance, menuLerpSpeed);
        mainMenuPanel.transform.rotation = Quaternion.Lerp(mainMenuPanel.transform.rotation, playerCamera.transform.rotation, menuLerpSpeed);
        mainMenuPosition.y = mainMenuPosition.y < 0.5f ? 0.5f : mainMenuPosition.y;
        mainMenuPanel.transform.position = mainMenuPosition;
    }

    private void SetupPanels() {
        foreach (var panel in panels) {
            panel.SetActive(false);
        }

        panels[_currentPanelNumber].SetActive(true);
    }

    public void GoToPanel(int newPanelNumber) {
        panels[_currentPanelNumber].SetActive(false);
        _currentPanelNumber = newPanelNumber;
        panels[_currentPanelNumber].SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void OnEnable() {
        openCloseInput.action.performed += OpenCloseMenu;
    }

    private void OnDisable() {
        openCloseInput.action.performed -= OpenCloseMenu;
    }

    private void OpenCloseMenu(InputAction.CallbackContext obj) {
        if (!_isMenuActive) {
            _isMenuActive = true;
            mainMenuPanel.transform.position = playerCamera.transform.position + playerCamera.transform.forward * menuDistance;
            var rotationVector = mainMenuPanel.transform.rotation.eulerAngles;
            rotationVector.y = playerCamera.transform.rotation.eulerAngles.y;
            mainMenuPanel.transform.rotation = Quaternion.Euler(rotationVector);
            mainMenuPanel.SetActive(true);
        }
        else {
            _isMenuActive = false;
            mainMenuPanel.SetActive(false);
            keyboardManager.EmptyKeyboardInputField();
        }
    }
    public void EnableAvatarSelectionPlatform() {
        avatarSelectionPlatform.SetActive(true);
    }
    public void DisableAvatarSelectionPlatform() {
        avatarSelectionPlatform.SetActive(false);
    }
}