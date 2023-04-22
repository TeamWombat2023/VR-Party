using UnityEngine;

public class CrawlAndJumpManager : MonoBehaviour {
    [Space] [SerializeField] public GameObject roomCam;

    private void Start() {
        SpawnPlayersWithDelay();
    }

    private void SpawnPlayersWithDelay() {
        if (PlayerManager.LocalPlayerPhotonView.IsMine) {
            PlayerManager.LocalXROrigin.transform.position = Vector3.zero + Vector3.left *
                GameManager.gameManager.GetPlayerIndex(PlayerManager.LocalPlayerPhotonView.Owner.NickName);
            PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
            PlayerManager.LocalPlayerInstance.GetComponent<Rigidbody>().isKinematic = false;
            PlayerManager.LocalPlayerInstance.SetActive(false);
            Invoke("SpawnPlayer", 5);
        }
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }

    private void Update() {
        if (HasAllPlayersFellDown() && PlayerManager.LocalPlayerPhotonView.IsMine) {
            PlayerManager.LocalPlayerInstance.GetComponent<Rigidbody>().isKinematic = true;
            GameManager.gameManager.OrderPlayersAndSetNewScores("CrawlAndJump");
            PlayerManager.OpenScoreboard();
        }
    }

    private bool HasAllPlayersFellDown() {
        foreach (var player in GameManager.gameManager.GetPlayers())
            if (!player.CustomProperties.ContainsKey("CrawlAndJump"))
                return false;
        return true;
    }
}