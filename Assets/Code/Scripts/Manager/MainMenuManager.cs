using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour {
    private int _currentPanelNumber;
    private bool _isMenuActive;

    public List<GameObject> panels;

    [SerializeField] private InputActionReference openCloseInput;

    [SerializeField] private GameObject menuGameObject;

    void Start() {
        if (menuGameObject != null)
            menuGameObject.SetActive(_isMenuActive);

        SetupPanels();
    }

    void SetupPanels() {
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
            menuGameObject.SetActive(true);
        }
        else {
            _isMenuActive = false;
            menuGameObject.SetActive(false);
        }
    }
}