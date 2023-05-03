using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks {
    public static GameObject LocalPlayerInstance { get; set; }
    public int health = 100;
    private static PlayerUIManager _playerUIManager;
    public static GameObject LocalXROrigin;
    public static PhotonView LocalPlayerPhotonView;
    public static Player MasterClient;
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
            LocalPlayerPhotonView.Owner.SetCustomProperties(new Hashtable {
                { "IsImmortal", true },
                { "FPS", 0.0 },
                { "CrawlAndJump", 0.0 },
                { "Fruit Ninja", 0.0 },
                { "Plane Game", 0.0 },
                { "Labyrinth", 0.0 },
                { "Score", 0 },
                { "HasScoreSet", false },
                { "PickupCount", 0 }
            });
            _playerUIManager = transform.GetChild(0).transform.GetChild(3).GetComponent<PlayerUIManager>();
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void SetVariables(GameObject player) {
        LocalXROrigin = player.transform.GetChild(0).gameObject;
        LocalAvatar = player.transform.GetChild(1).gameObject;
        LocalAvatarBody = LocalAvatar.transform.GetChild(0).gameObject;
        LocalAvatarBody = LocalAvatar.transform.GetChild(1).gameObject;
        LocalAvatarLeftHand = LocalAvatar.transform.GetChild(2).gameObject;
        LocalAvatarRightHand = LocalAvatar.transform.GetChild(3).gameObject;
        LocalPlayerPhotonView = player.GetComponent<PhotonView>();
        _playerUIManager = player.transform.GetChild(0).transform.GetChild(3).GetComponent<PlayerUIManager>();
    }

    private void Start() {
        if (photonView.IsMine) FindMasterClient();
    }

    private void FindMasterClient() {
        foreach (var player in PhotonNetwork.PlayerList)
            if (player.IsMasterClient) {
                MasterClient = player;
                break;
            }
    }

    public static void SetWeapon(bool isGunEnabled) {
        LocalAvatarRightHand.transform.GetChild(0).GetComponent<Weapon>().enabled = isGunEnabled;
    }

    public static void AddScoreToMiniGame(string miniGame, double score) {
        if (LocalPlayerPhotonView.IsMine) {
            var prevScore = (double)LocalPlayerPhotonView.Owner.CustomProperties[miniGame];
            LocalPlayerPhotonView.Owner.SetCustomProperties(new Hashtable {
                { miniGame, prevScore + score }
            });
        }
    }

    public static void SetScore(int amount) {
        if (LocalPlayerPhotonView.IsMine) LocalPlayerPhotonView.Owner.SetScore(amount);
    }

    public static void OpenScoreboard() {
        if (LocalPlayerPhotonView.IsMine) _playerUIManager.OpenScoreBoard();
    }

    public static void CloseScoreboard() {
        if (LocalPlayerPhotonView.IsMine) _playerUIManager.CloseScoreBoard();
    }

    public static void ActivateHandsIn(string gameName) {
        if (LocalPlayerPhotonView.IsMine) LocalPlayerPhotonView.RPC("ActivateHands", RpcTarget.All, gameName);
    }

    [PunRPC]
    public void FPSDamageTake(int damage) {
        health -= damage;

        if (health <= 0) {
            gameObject.SetActive(false);
            FPSNetworkManager.instance.RespawnWithDelay(gameObject);
        }
    }

    [PunRPC]
    public void EnableAllPlayers() {
        gameObject.SetActive(true);
    }

    [PunRPC]
    public void ActivateHands(string gameName) {
        var rightHand = gameObject.transform.GetChild(1).transform.GetChild(3).transform.GetChild(0).transform;
        switch (gameName) {
            case "Fruit Ninja":
                rightHand.GetChild(0).gameObject.SetActive(false);
                rightHand.GetChild(1).gameObject.SetActive(true);
                rightHand.GetChild(2).gameObject.SetActive(false);
                rightHand.GetChild(3).gameObject.SetActive(false);
                break;
            case "FPS":
                rightHand.GetChild(0).gameObject.SetActive(false);
                rightHand.GetChild(1).gameObject.SetActive(false);
                rightHand.GetChild(2).gameObject.SetActive(true);
                rightHand.GetChild(3).gameObject.SetActive(true);
                break;
            default:
                rightHand.GetChild(0).gameObject.SetActive(true);
                rightHand.GetChild(1).gameObject.SetActive(false);
                rightHand.GetChild(2).gameObject.SetActive(false);
                rightHand.GetChild(3).gameObject.SetActive(false);
                break;
        }
    }
}