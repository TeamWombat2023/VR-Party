using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour{

    void Start() {
        if (PhotonNetwork.IsConnectedAndReady) {
            // PhotonNetwork.Instantiate("Avatar", Vector3.zero, Quaternion.identity);
        }
    }

    void Update() {
        
    }
}
