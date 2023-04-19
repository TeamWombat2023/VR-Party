using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks {
    public static GameObject LocalPlayerInstance { get; set; }
    public int health = 100;
    public int score = 0;
    public bool isImmortal = false;
    public static GameObject LocalXROrigin;
    public static PhotonView LocalPlayerPhotonView;

    private void Awake() {
        if (photonView.IsMine) {
            LocalPlayerInstance = gameObject;
            LocalPlayerInstance.GetComponent<Rigidbody>().isKinematic = false;
            LocalXROrigin = transform.GetChild(0).gameObject;
            LocalPlayerPhotonView = photonView;
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

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("ScorePickup")){
            other.gameObject.SetActive(false);
            score += 10;
            Debug.Log("Score: " + score);
        }

    }

}