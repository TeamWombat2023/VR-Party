using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    Canvas canvas = null;
    MainMenuManager menuManager = null;
    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void SetupCanvas(MainMenuManager menuManager)
    {
        this.menuManager = menuManager;
        HideCanvas();
    }
    void ShowCanvas()
    {
        canvas.enabled = true;
    }
    void HideCanvas()
    {
        canvas.enabled = false;
    }
}
