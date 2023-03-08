using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public GameObject camera;
    public GameObject XRorg;

    public void IsLocalPlayer(){
        camera.SetActive(true);
        (XRorg.GetComponent("InputActionManager") as MonoBehaviour).enabled = true;
    } 
}
