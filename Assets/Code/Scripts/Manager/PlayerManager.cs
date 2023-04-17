using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks {
    public static GameObject LocalPlayerInstance;

    private void Awake() {
        if (photonView.IsMine) LocalPlayerInstance = gameObject;
        DontDestroyOnLoad(gameObject);
    }
}