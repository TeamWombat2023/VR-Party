using System;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.InputSystem;
using InputDevice = UnityEngine.XR.InputDevice;

public class Weapon : MonoBehaviourPunCallbacks {
    public int damage;
    public float fireRate;
    private float _nextFire;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform raycastOrigin;

    [SerializeField] private InputActionReference shootGunTrigger;

    //UnityEngine.XR.InputDevice device;
    private InputDevice device;

    [Header("VFX")] public GameObject hitVFX;

    private void Awake() {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && photonView.IsMine)
            device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    private void Update() {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && photonView.IsMine) {
            if (device == null || !device.isValid) device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            if (_nextFire > 0) _nextFire -= Time.deltaTime;
        }
    }

    private void OnEnable() {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && photonView.IsMine)
            shootGunTrigger.action.performed += ShootGun;
    }

    private void ShootGun(InputAction.CallbackContext context) {
        if (_nextFire <= 0) Fire();
    }

    private void Fire() {
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward), out hit,
                Mathf.Infinity, targetLayer)) {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);
            device.SendHapticImpulse(0, .7f, .25f);

            if (hit.collider.CompareTag("Body") || hit.collider.CompareTag("Head"))
                hit.transform.gameObject.GetComponentInParent<PhotonView>().RPC("FPSDamageTake", RpcTarget.All, damage);

            _nextFire = 1 / fireRate;
            Debug.Log($"<color=green>Trigger button is {hit.transform.name} pressed</color>");
        }
    }
}