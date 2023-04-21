using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks {
    public static GameObject LocalPlayerInstance { get; set; }
    public int health = 100;
    public bool isImmortal = false;
    private static PlayerUIManager _playerUIManager;
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
            _playerUIManager = transform.GetChild(0).transform.GetChild(3).GetComponent<PlayerUIManager>();
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

    public static void AddScore(int amount) {
        if (LocalPlayerPhotonView.IsMine) LocalPlayerPhotonView.Owner.AddScore(amount);
    }

    public static int GetScore() {
        if (LocalPlayerPhotonView.IsMine) return LocalPlayerPhotonView.Owner.GetScore();

        return -1;
    }

    public static void SetScore(int amount) {
        if (LocalPlayerPhotonView.IsMine) LocalPlayerPhotonView.Owner.SetScore(amount);
    }

    public static void OpenScoreboard() {
        if (LocalPlayerPhotonView.IsMine) _playerUIManager.OpenScoreBoard();
    }


    [PunRPC]
    public void FPSDamageTake(int damage) {
        if (!isImmortal) {
            health -= damage;

            if (health <= 0) {
                gameObject.SetActive(false);
                FPSNetworkManager.instance.RespawnWithDelay(gameObject);
            }
        }
    }
}