using UnityEngine;
using System.Collections;
using Photon.Pun;

public class FPSNetworkManager : MonoBehaviour {
    public static FPSNetworkManager instance;

    [Space] [SerializeField] public GameObject roomCam;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Debug.Log("JOINED MINIGAME");
        SpawnPlayersWithDelay();
    }

    public void SpawnPlayersWithDelay() {
        PlayerManager.ActivateHands("FPS");
        PlayerManager.LocalXROrigin.transform.position = Vector3.zero;
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
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
        _player.GetComponent<PlayerManager>().health = 100;
        _player.SetActive(true);
        if (_player.GetComponent<PhotonView>().Owner.NickName ==
            PlayerManager.LocalPlayerInstance.GetComponent<PhotonView>().Owner.NickName)
            roomCam.SetActive(false);
        //StartCoroutine(MakePlayerMortal(_player));
    }

    private IEnumerator MakePlayerMortal(GameObject _player) {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("MORTAL YAPTI");
        // _player.GetComponent<PlayerSetup>().OpenWeapon();
        _player.GetComponent<PlayerManager>().isImmortal = false;
    }
}