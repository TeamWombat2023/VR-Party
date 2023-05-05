using UnityEngine;

public class CrawlAndJumpManager : MonoBehaviour {
    [Space] [SerializeField] public GameObject roomCam;
    
    public float gameDuration = 60f;

    private void Start() {
        SpawnPlayersWithDelay();
        Invoke(nameof(FinishGame), gameDuration);
    }

    private void SpawnPlayersWithDelay() {
        if (PlayerManager.LocalPlayerPhotonView.IsMine) {
            PlayerManager.LocalXROrigin.transform.position = Vector3.zero + Vector3.left * 2 *
                GameManager.gameManager.GetPlayerIndex(PlayerManager.LocalPlayerPhotonView.Owner.NickName);
            PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
            // PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            PlayerManager.LocalPlayerInstance.GetComponent<Rigidbody>().isKinematic = false;
            PlayerManager.LocalPlayerInstance.SetActive(false);
            Invoke("SpawnPlayer", 5);
        }
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }

    public static void FinishGame() {
        if (PlayerManager.LocalPlayerPhotonView.IsMine) {
            // PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            GameManager.gameManager.OrderPlayersAndSetNewScores("CrawlAndJump");
            PlayerManager.OpenScoreboard();
        }
    }
}