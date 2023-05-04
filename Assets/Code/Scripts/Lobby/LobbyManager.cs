using UnityEngine;

public class LobbyManager : MonoBehaviour {
    public GameObject startButtonHolder;
    public GameObject scoreBoardHolder;
    public Transform scoreBoardContent;
    public LobbyScoreBoardEntry scoreBoardEntry;


    private Animator _startButtonHolderAnimator;
    private string currentState;
    private const string BUTTON_IDLE = "idle";
    private const string BUTTON_PRESSED = "button_pressed";

    private void Start() {
        _startButtonHolderAnimator = startButtonHolder.GetComponent<Animator>();
        if (GameManager.IsGameFinished) ListScoreBoard();
        else scoreBoardHolder.SetActive(false);
    }


    public void StartButtonPressed() {
        GameManager.gameManager.StartNextGame();
        AnimateStartButton();
    }


    public void StartButtonSelected() {
        GameManager.gameManager.StartNextGame();
        AnimateStartButton();
    }


    private void AnimateStartButton() {
        ChangeAnimationState(BUTTON_PRESSED);
        ChangeAnimationState(BUTTON_IDLE);
    }

    private void ChangeAnimationState(string newState) {
        if (currentState == newState) currentState = BUTTON_IDLE;
        _startButtonHolderAnimator.Play(newState);
        currentState = newState;
    }

    private void ListScoreBoard() {
        scoreBoardHolder.SetActive(true);
        var totalScores = GameManager.gameManager.GetScores();
        var fps = GameManager.gameManager.GetScoresFor("FPS");
        var plane = GameManager.gameManager.GetScoresFor("Plane Game");
        var labyrinth = GameManager.gameManager.GetScoresFor("Labyrinth");
        var fruitNinja = GameManager.gameManager.GetScoresFor("Fruit Ninja");
        var crawlAndJump = GameManager.gameManager.GetScoresFor("CrawlAndJump");
        var i = 1;
        foreach (var score in totalScores) {
            var scoreBoardElementInstance = Instantiate(scoreBoardEntry, scoreBoardContent);
            scoreBoardElementInstance.SetScoreInfo(i, score.Key, fps[score.Key], plane[score.Key],
                labyrinth[score.Key], fruitNinja[score.Key], crawlAndJump[score.Key], score.Value);
            i++;
        }
    }
}