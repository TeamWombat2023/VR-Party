using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Keyboard = VRKeys.Keyboard;

public class MainMenuManager : MonoBehaviour
{
    public int currentPanelNumber;
    public List<GameObject> panels;
    private bool _isMenuActive;
    
    [SerializeField]
    InputActionReference openCloseInput;
    
    [SerializeField]
    GameObject menuGameObject;

    void Start()
    {
        if (menuGameObject != null) 
            menuGameObject.SetActive(_isMenuActive);
        
        SetupPanels();
    }

    void SetupPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[currentPanelNumber].SetActive(true);
    }
    
    public void GoNextPanel()
    {
        panels[currentPanelNumber].SetActive(false);
        currentPanelNumber++;
        panels[currentPanelNumber].SetActive(true);
    }
    
    public void GoPreviousPanel()
    {
        panels[currentPanelNumber].SetActive(false);
        currentPanelNumber--;
        panels[currentPanelNumber].SetActive(true);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    private void OnEnable()
    {
        openCloseInput.action.performed += OpenCloseMenu;
    }
    
    private void OnDisable()
    {
        openCloseInput.action.performed -= OpenCloseMenu;
    }
    
    private void OpenCloseMenu(InputAction.CallbackContext obj)
    {
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
