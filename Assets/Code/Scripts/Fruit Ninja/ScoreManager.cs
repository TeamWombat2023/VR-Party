using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private int _score;
    public TMP_Text scoreText;

    private void Start() {
        scoreText.gameObject.SetActive(false);
    }

    public void IncrementScore() {
        _score++;
    }

    public void FinishGame() {
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: " + _score;
    }
}