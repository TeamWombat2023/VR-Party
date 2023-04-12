using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Photon.Pun;
using Photon.Realtime;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float fireRate;
    private float nextFire;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Transform raycastOrigin;
    //UnityEngine.XR.InputDevice device;
    InputDevice device;

    [Header("VFX")]
    public GameObject hitVFX;

    void Start(){
        Debug.Log("Start initialized1");
        //var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        //UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        //device = rightHandDevices[0];
        //device =  UnityEngine.XR.InputDeviceRole.RightHanded;
        InputDeviceCharacteristics rightControllerCharacteristics = 
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        Debug.Log("Start initialized");
    }
    // Update is called once per frame
    void Update()
    {
        if (device == null || !device.isValid)
        {
            device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        if (device != null && device.isValid)
        {
            
            if(nextFire > 0){
                nextFire -= Time.deltaTime;
            }

            
            //UnityEngine.XR.InputDevice device = UnityEngine.InputSystem.XR.GetDevice<XRController>(CommonUsages.RightHand);
            Debug.Log("update initialized");
            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                if(nextFire <= 0){
                    Debug.Log("should fire");
                    Fire();
                }
                
            }
        }
    }

    void Fire(){
                    RaycastHit hit;

                    if(Physics.Raycast(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward),out hit ,Mathf.Infinity ,targetLayer)){
                        PhotonNetwork.Instantiate(hitVFX.name,hit.point, Quaternion.identity);
                        device.SendHapticImpulse(0,.7f,.25f);

                        if(hit.transform.gameObject.GetComponent<FPSPlayerHealth>()){
                            hit.transform.gameObject.GetComponent<PhotonView>().RPC("FPSDamageTake", RpcTarget.All, damage);
                        }

                        nextFire = 1 / fireRate;
                        Debug.Log($"<color=green>Trigger button is {hit.transform.name} pressed</color>");
                    }
    }
}
