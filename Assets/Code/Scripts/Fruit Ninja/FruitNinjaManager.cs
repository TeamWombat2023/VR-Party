using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class FruitNinjaManager : MonoBehaviour {
    [Space] [SerializeField] public GameObject roomCam;

    // private TMP_Text _scoreText;
    private FruitSpawner _fruitSpawner;
    public float gameDuration = 60f;

    private void Start() {
        _fruitSpawner = FindObjectOfType<FruitSpawner>();
        // _scoreText.gameObject.SetActive(false);
        Invoke(nameof(FinishGame), gameDuration);
        SpawnPlayersWithDelay();
    }

    private void SpawnPlayersWithDelay() {
        PlayerManager.ActivateHands("Fruit Ninja");
        PlayerManager.LocalXROrigin.transform.position = Vector3.zero;
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }

    public void IncrementScore() {
        PlayerManager.LocalPlayerPhotonView.Owner.AddScore(1);
    }

    public void FinishGame() {
        // _scoreText.gameObject.SetActive(true);
        // _scoreText.text = "Score: " + PlayerManager.LocalPlayerPhotonView.Owner.GetScore();
        int[] scores = GameManager.gameManager.GetScores();
        for (int i = 0; i < scores.Length ; i++) {
            Debug.Log(scores[i]);
        }
    }

    public void GameOver() {
        _fruitSpawner.gameObject.SetActive(false);
        // _scoreText.gameObject.SetActive(true);
        // _scoreText.text = "Game Over";
    }
}