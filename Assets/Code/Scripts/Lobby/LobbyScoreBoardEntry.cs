using TMPro;
using UnityEngine;

public class LobbyScoreBoardEntry : MonoBehaviour {
    [SerializeField] private TMP_Text playerCountText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text fpsScoreText;
    [SerializeField] private TMP_Text planeScoreText;
    [SerializeField] private TMP_Text labyrinthScoreText;
    [SerializeField] private TMP_Text fruitNinjaScoreText;
    [SerializeField] private TMP_Text crawlAndJumpScoreText;
    [SerializeField] private TMP_Text playerScoreTotalText;

    public void SetScoreInfo(int playerCount, string playerName, double fpsScore, double planeScore,
        double labyrinthScore, double fruitNinjaScore, double crawlAndJumpScore, int playerScoreTotal) {
        playerCountText.text = playerCount.ToString();
        playerNameText.text = playerName;
        fpsScoreText.text = fpsScore.ToString();
        planeScoreText.text = planeScore.ToString();
        labyrinthScoreText.text = labyrinthScore.ToString();
        fruitNinjaScoreText.text = fruitNinjaScore.ToString();
        crawlAndJumpScoreText.text = crawlAndJumpScore.ToString();
        playerScoreTotalText.text = playerScoreTotal.ToString();
    }
}