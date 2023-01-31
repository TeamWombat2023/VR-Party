using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button exitButton;
    public int currentPanelNumber = 0;
    public List<GameObject> panels = new List<GameObject>();
    private List<Panel> panelHistory = new List<Panel>();

    void Start()
    {
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
}
