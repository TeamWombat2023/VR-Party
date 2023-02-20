using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkedGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks {
    private PhotonView m_photonView;
    private Rigidbody _rb;
    private bool _isGrabbed;

    private void Awake() {
        m_photonView = GetComponent<PhotonView>();
    }

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (_isGrabbed) {
            _rb.isKinematic = true;
            gameObject.layer = LayerMask.NameToLayer("Grab Interactable");
        }
        else {
            _rb.isKinematic = false;
            gameObject.layer = LayerMask.NameToLayer("In Hand");
        }
    }

    private void TransferOwnership() {
        m_photonView.RequestOwnership();
    }

    public void OnSelectEntered() {
        m_photonView.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);

        if (!Equals(m_photonView.Owner, PhotonNetwork.LocalPlayer))
            TransferOwnership();
    }


<<<<<<< HEAD
    public void OnSelectExited() {
=======
    public void OnSelectedExited() {
>>>>>>> Added grabbing synchronization through network feature
        m_photonView.RPC("StopNetworkGrabbing", RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer) {
        if (targetView != m_photonView) return;

        m_photonView.TransferOwnership(requestingPlayer);
    }

<<<<<<< HEAD
    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner) {
=======
    public void OnOwnershipTransferred(PhotonView targetView, Player previousOwner) {
>>>>>>> Added grabbing synchronization through network feature
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest) {
    }

    [PunRPC]
    public void StartNetworkGrabbing() {
        _isGrabbed = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing() {
        _isGrabbed = false;
    }
}