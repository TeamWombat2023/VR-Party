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
    private bool _isAvatarSelectionPlatformActive;

    private void Start() {
        mainMenuPanel.SetActive(_isMenuActive);
        avatarSelectionPlatform.SetActive(_isAvatarSelectionPlatformActive);
        SetupPanels();
    }

    private void FixedUpdate() {
        if (!_isMenuActive || keyboardManager.IsKeyboardActive() || _isAvatarSelectionPlatformActive) return;

        var mainMenuPosition = mainMenuPanel.transform.position;
        mainMenuPosition = Vector3.Lerp(mainMenuPosition,
            playerCamera.transform.position + playerCamera.transform.forward * menuDistance, menuLerpSpeed);
        mainMenuPanel.transform.rotation = Quaternion.Lerp(mainMenuPanel.transform.rotation,
            playerCamera.transform.rotation, menuLerpSpeed);
        mainMenuPosition.y = mainMenuPosition.y < 0.5f ? 0.5f : mainMenuPosition.y;
        mainMenuPanel.transform.position = mainMenuPosition;
    }

    private void SetupPanels() {
        foreach (var panel in panels) panel.SetActive(false);

        panels[_currentPanelNumber].SetActive(true);
    }

    public void GoToPanel(int newPanelNumber) {
        if (keyboardManager.IsKeyboardActive()) keyboardManager.DisableVRKeyboard();
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
            mainMenuPanel.transform.position =
                playerCamera.transform.position + playerCamera.transform.forward * menuDistance;
            var rotationVector = mainMenuPanel.transform.rotation.eulerAngles;
            rotationVector.y = playerCamera.transform.rotation.eulerAngles.y;
            mainMenuPanel.transform.rotation = Quaternion.Euler(rotationVector);
            mainMenuPanel.SetActive(true);
            avatarSelectionPlatform.SetActive(_isAvatarSelectionPlatformActive);
        }
        else {
            _isMenuActive = false;
            mainMenuPanel.SetActive(false);
            keyboardManager.EmptyKeyboardInputField();
            keyboardManager.DisableVRKeyboard();
            avatarSelectionPlatform.SetActive(false);
        }
    }

    public void EnableAvatarSelectionPlatform() {
        _isAvatarSelectionPlatformActive = true;
        avatarSelectionPlatform.SetActive(true);
    }

    public void DisableAvatarSelectionPlatform() {
        _isAvatarSelectionPlatformActive = false;
        avatarSelectionPlatform.SetActive(false);
    }
}