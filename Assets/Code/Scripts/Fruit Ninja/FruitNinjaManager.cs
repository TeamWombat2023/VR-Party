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
        PlayerManager.ActivateHands("Fruit Ninja");
        PlayerManager.LocalXROrigin.transform.position = Vector3.zero + Vector3.left *
            GameManager.gameManager.GetPlayerIndex(PlayerManager.LocalPlayerPhotonView.Owner.NickName);
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        if (PlayerManager.LocalPlayerPhotonView.IsMine)
            Instantiate(fruitSpawner,
                PlayerManager.LocalXROrigin.transform.position + Vector3.forward * 5, Quaternion.identity);
        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }

    public void IncrementScore() {
        PlayerManager.AddScore(1);
    }

    public void DecreaseScore() {
        PlayerManager.AddScore(-2);
    }

    public void FinishGame() {
        var scores = GameManager.gameManager.GetScores();
        for (var i = 0; i < scores.Length; i++) Debug.Log(scores[i]);
    }
}