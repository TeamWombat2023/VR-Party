using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class FruitNinjaManager : MonoBehaviour {
    [Space] [SerializeField] public GameObject roomCam;

    private FruitSpawner _fruitSpawner;
    public float gameDuration = 60f;

    private void Start() {
        _fruitSpawner = FindObjectOfType<FruitSpawner>();
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
        PlayerManager.AddScore(1);
    }

    public void DecreaseScore() {
        PlayerManager.AddScore(-2);
    }

    public void FinishGame() {
        var scores = GameManager.gameManager.GetScores();
        for (var i = 0; i < scores.Length; i++) Debug.Log(scores[i]);
    }

    public void GameOver() {
        _fruitSpawner.gameObject.SetActive(false);
    }
}