using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private int _score;
    public TMP_Text scoreText;
    private FruitSpawner _fruitSpawner;
    public float gameDuration = 60f;

    private void Start() {
        _fruitSpawner = FindObjectOfType<FruitSpawner>();
        scoreText.gameObject.SetActive(false);
        Invoke(nameof(FinishGame), gameDuration);
    }

    public void IncrementScore() {
        _score++;
    }

    public void FinishGame() {
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: " + _score;
    }

    public void GameOver() {
        _fruitSpawner.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Game Over";
    }
}