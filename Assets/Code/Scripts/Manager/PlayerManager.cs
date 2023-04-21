using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks {
    public static GameObject LocalPlayerInstance { get; set; }
    public int health = 100;
    public bool isImmortal = false;
    public static GameObject LocalXROrigin;
    public static PhotonView LocalPlayerPhotonView;
    public static GameObject LocalAvatar;
    public static GameObject LocalAvatarBody;
    public static GameObject LocalAvatarHead;
    public static GameObject LocalAvatarLeftHand;
    public static GameObject LocalAvatarRightHand;

    private void Awake() {
        if (photonView.IsMine) {
            LocalPlayerInstance = gameObject;
            LocalXROrigin = transform.GetChild(0).gameObject;
            LocalAvatar = transform.GetChild(1).gameObject;
            LocalAvatarBody = LocalAvatar.transform.GetChild(0).gameObject;
            LocalAvatarBody = LocalAvatar.transform.GetChild(1).gameObject;
            LocalAvatarLeftHand = LocalAvatar.transform.GetChild(2).gameObject;
            LocalAvatarRightHand = LocalAvatar.transform.GetChild(3).gameObject;
            LocalPlayerPhotonView = photonView;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void ActivateHands(string gameName) {
        switch (gameName) {
            case "Fruit Ninja":
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
                break;
            case "FPS":
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
                break;
            default:
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                LocalAvatarRightHand.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
                break;
        }
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