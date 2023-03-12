using Photon.Pun;
using UnityEngine;

public class FPSSceneEnter : MonoBehaviourPunCallbacks {
    private void OnTriggerEnter(Collider other) {
        PhotonNetwork.LoadLevel("FPS Scene");
    }
}