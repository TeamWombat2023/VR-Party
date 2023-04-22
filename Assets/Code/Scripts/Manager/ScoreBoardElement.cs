using TMPro;
using UnityEngine;

public class ScoreBoardElement : MonoBehaviour {
    [SerializeField] private TMP_Text playerCountText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerScoreText;

    public void SetScoreInfo(int playerCount, string playerName, int playerScore) {
        playerCountText.text = playerCount.ToString();
        playerNameText.text = playerName;
        playerScoreText.text = playerScore.ToString();
    }
}