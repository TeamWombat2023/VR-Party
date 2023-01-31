using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button exitButton;
    public int currentPanelNumber;
    public List<GameObject> panels;
    private bool isMenuActive;
    
    [SerializeField]
    InputActionReference openCloseInput;
    
    [SerializeField]
    GameObject menuGameObject;

    void Start()
    {
        if (menuGameObject != null) 
            menuGameObject.SetActive(isMenuActive);
        
        SetupPanels();
        exitButton.onClick.AddListener((() => { Application.Quit(); }));
    }

    void SetupPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[currentPanelNumber].SetActive(true);
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
        if (!isMenuActive) {
            isMenuActive = true;
            menuGameObject.SetActive(true);
        }
        else {
            isMenuActive = false;
            menuGameObject.SetActive(false);
        }
    }
}
