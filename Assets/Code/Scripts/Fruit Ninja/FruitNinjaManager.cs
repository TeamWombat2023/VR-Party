using UnityEngine;

public class FruitNinjaManager : MonoBehaviour {
    [Space] [SerializeField] public GameObject roomCam;

    public GameObject fruitSpawner;
    public float gameDuration = 60f;

    private void Start() {
        Invoke(nameof(FinishGame), gameDuration);
        SpawnPlayersWithDelay();
    }

    private void SpawnPlayersWithDelay() {
        PlayerManager.ActivateHandsIn("Fruit Ninja");
        PlayerManager.LocalXROrigin.transform.position = Vector3.zero + Vector3.left *
            GameManager.gameManager.GetPlayerIndex(PlayerManager.LocalPlayerPhotonView.Owner.NickName);
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;

        if (PlayerManager.LocalPlayerPhotonView.IsMine) {
            var localFruitSpawner = Instantiate(fruitSpawner,
                PlayerManager.LocalXROrigin.transform.position + Vector3.forward, Quaternion.identity);
            Destroy(localFruitSpawner, gameDuration);
        }

        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }

    public void IncrementScore() {
        PlayerManager.AddScoreToMiniGame("Fruit Ninja", 1);
    }

    public void DecreaseScore() {
        PlayerManager.AddScoreToMiniGame("Fruit Ninja", -2);
    }

    public void FinishGame() {
        GameManager.gameManager.OrderPlayersAndSetNewScores("Fruit Ninja");
        PlayerManager.ActivateHandsIn("");
        PlayerManager.OpenScoreboard();
    }
}