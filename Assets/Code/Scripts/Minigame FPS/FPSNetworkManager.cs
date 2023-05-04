using UnityEngine;
using System.Collections;
using Photon.Pun;

public class FPSNetworkManager : MonoBehaviour {
    public static FPSNetworkManager instance;

    [Space] [SerializeField] public GameObject roomCam;
    public float gameDuration = 60f;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Debug.Log("JOINED MINIGAME");
        Invoke(nameof(FinishGame), gameDuration);
        SpawnPlayersWithDelay();
    }

    public void SpawnPlayersWithDelay() {
        PlayerManager.SetWeapon(false);
        PlayerManager.LocalXROrigin.transform.position = Vector3.zero + Vector3.left *
            GameManager.gameManager.GetPlayerIndex(PlayerManager.LocalPlayerPhotonView.Owner.NickName);
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        PlayerManager.LocalPlayerInstance.GetComponent<Rigidbody>().isKinematic = false;
        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        PlayerManager.LocalPlayerPhotonView.Owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
            { "IsImmortal", true }
        });
        roomCam.SetActive(false);
        StartCoroutine(MakePlayerMortal(PlayerManager.LocalPlayerInstance));
    }


    public void RespawnWithDelay(GameObject _player) {
        if (_player.GetComponent<PhotonView>().Owner.NickName == PlayerManager.LocalPlayerPhotonView.Owner.NickName)
            roomCam.SetActive(true);
        StartCoroutine(RespawnPlayer(_player));
    }

    private IEnumerator RespawnPlayer(GameObject _player) {
        yield return new WaitForSeconds(3.0f);
        _player.transform.GetChild(0).gameObject.transform.position = Vector3.zero;
        _player.transform.GetChild(0).gameObject.transform.rotation = Quaternion.identity;
        var _playerManager = _player.GetComponent<PlayerManager>();
        _playerManager.health = 100;
        _player.SetActive(true);

        if (_player.GetComponent<PhotonView>().Owner.NickName == PlayerManager.LocalPlayerPhotonView.Owner.NickName) {
            roomCam.SetActive(false);
            PlayerManager.SetWeapon(false);
            PlayerManager.ActivateHandsIn("");
            _player.GetComponent<PhotonView>().Owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
                { "IsImmortal", true }
            });
            StartCoroutine(MakePlayerMortal(_player));
        }
    }

    private IEnumerator MakePlayerMortal(GameObject _player) {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("MORTAL YAPTI");
        PlayerManager.SetWeapon(true);
        PlayerManager.ActivateHandsIn("FPS");
        _player.GetComponent<PhotonView>().Owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
            { "IsImmortal", false }
        });
    }

    public void FinishGame() {
        if (PlayerManager.LocalPlayerPhotonView.IsMine) {
            PlayerManager.LocalPlayerPhotonView.RPC("EnableAllPlayers", RpcTarget.All);
            GameManager.gameManager.OrderPlayersAndSetNewScores("FPS");
            PlayerManager.SetWeapon(false);
            PlayerManager.ActivateHandsIn("");
            PlayerManager.OpenScoreboard();
        }
    }
}