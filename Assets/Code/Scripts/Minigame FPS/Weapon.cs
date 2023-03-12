using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float fireRate;
    private float nextFire;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Transform raycastOrigin;
    // Update is called once per frame
    void Update()
    {

        if(nextFire > 0){
            nextFire -= Time.deltaTime;
        }
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        UnityEngine.XR.InputDevice device = rightHandDevices[0];
        //UnityEngine.XR.InputDevice device = UnityEngine.InputSystem.XR.GetDevice<XRController>(CommonUsages.RightHand);
        bool triggerValue;
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton,out triggerValue) && triggerValue)
                {
                    RaycastHit hit;

                    if(nextFire <= 0 && Physics.Raycast(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward),out hit ,Mathf.Infinity ,targetLayer)){
                        device.SendHapticImpulse(0,.5f,.25f);
                        nextFire = 1 / fireRate;
                        Debug.Log($"<color=green>Trigger button is {hit.transform.name} pressed</color>");
                    }
                }
    }

    void Fire(){

    }
}
