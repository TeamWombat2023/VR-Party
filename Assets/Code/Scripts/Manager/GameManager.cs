using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks {
    public static GameManager gameManager { get; private set; }
    [SerializeField] private GameObject playerPrefab;

    public static bool IsGameFinished = false;
    private int _playerCount;
    private bool[] _isPlayed;

    private void Awake() {
        if (gameManager == null) {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        _isPlayed = new bool[5];
    }

    public string GetCurrentSceneName() {
        var scene = SceneManager.GetActiveScene();
        return scene.name;
    }

    public void LoadScene(string sceneName) {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(sceneName);
    }

    public GameObject CreatePlayer() {
        var player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(player);
        return player;
    }

    public Player GetPlayer(string playerName) {
        foreach (var player in PhotonNetwork.PlayerList)
            if (player.NickName == playerName)
                return player;
        return null;
    }

    public Player[] GetPlayers() {
        return PhotonNetwork.PlayerList;
    }

    public Minigame GetRandomMinigame() {
        var random = Random.Range(0, 5);
        int i, j;
        for (i = random, j = 0; _isPlayed[i] && j < 5; i++, j++) {
        }

        if (j < 5) {
            _isPlayed[i] = true;
            return (Minigame)random;
        }

        return Minigame.Lobby;
    }

    public void StartMinigame(Minigame minigame) {
        switch (minigame) {
            case Minigame.PlaneGame:
                LoadScene("PlaneGameScene");
                break;
            case Minigame.FruitNinja:
                LoadScene("Fruit Ninja");
                break;
            case Minigame.FPS:
                LoadScene("FPS Scene");
                break;
            case Minigame.Labyrinth:
                LoadScene("Labyrinth Scene");
                break;
            case Minigame.CrawlAndJump:
                LoadScene("Crawl and Jump");
                break;
            default:
                IsGameFinished = true;
                LoadScene("Lobby Scene");
                break;
        }
    }

    public void ResetMinigame() {
        for (var i = 0; i < _isPlayed.Length; i++) _isPlayed[i] = false;
    }

    // return all scores of all players
    public int[] GetScores() {
        var scores = new int[PhotonNetwork.PlayerList.Length];
        for (var i = 0; i < PhotonNetwork.PlayerList.Length; i++) scores[i] = PhotonNetwork.PlayerList[i].GetScore();

        return scores;
    }


    public enum Minigame {
        PlaneGame,
        FruitNinja,
        FPS,
        Labyrinth,
        CrawlAndJump,
        Lobby
    }
}