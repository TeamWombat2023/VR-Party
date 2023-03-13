using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class DetectVR : MonoBehaviour
{
    public GameObject XROrigin;

    public GameObject desktopCharacter;
    // Start is called before the first frame update
    void Start()
    {
        var xrSettings = XRGeneralSettings.Instance;
        if (xrSettings == null)
        {
            Debug.Log("Settings is null");
            return;
        }

        var xrManager = xrSettings.Manager;
        if (xrManager == null)
        {
            Debug.Log("xrManagerSettings is null");
            return;
        }
        
        var xrLoader = xrManager.activeLoader;
        if (xrLoader == null)
        {
            Debug.Log("XRLoader is null");
            XROrigin.SetActive(false);
            desktopCharacter.SetActive(true);
            return;
        }
        
        //Bu
        Debug.Log("XRLoader is not null");
        XROrigin.SetActive(true);
        desktopCharacter.SetActive(false);
        
        
        
    

    }

    
}
