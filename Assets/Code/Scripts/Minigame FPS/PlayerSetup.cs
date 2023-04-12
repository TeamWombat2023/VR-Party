using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public GameObject camera;
    public GameObject XRorg;
    public GameObject leftHand;
    public GameObject rightHand;

    public void IsLocalPlayer(){
        camera.SetActive(true);
        (XRorg.GetComponent("InputActionManager") as MonoBehaviour).enabled = true;
        //(XRorg.GetComponent("LocomotionSystem") as MonoBehaviour).enabled = true;
        (leftHand.GetComponent("ActionBasedController") as MonoBehaviour).enabled = true;
        (rightHand.GetComponent("ActionBasedController") as MonoBehaviour).enabled = true;
        (rightHand.GetComponent("Weapon") as MonoBehaviour).enabled = false;
    }

    public void OpenWeapon(){
        (rightHand.GetComponent("Weapon") as MonoBehaviour).enabled = true;
    } 
}
