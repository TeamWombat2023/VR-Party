using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks {
    public static GameObject LocalPlayerInstance { get; set; }
    public int health = 100;
    public bool isImmortal = false;
    public static GameObject LocalXROrigin;

    private void Awake() {
        if (photonView.IsMine) {
            LocalPlayerInstance = gameObject;
            LocalXROrigin = transform.GetChild(0).gameObject;
        }

        DontDestroyOnLoad(gameObject);
    }



    [PunRPC]
    public void FPSDamageTake(int _damage) {
        if (!isImmortal) {
            health -= _damage;

            if (health <= 0) {
                gameObject.SetActive(false);
                FPSNetworkManager.instance.RespawnWithDelay(gameObject);
            }
        }
    }
}