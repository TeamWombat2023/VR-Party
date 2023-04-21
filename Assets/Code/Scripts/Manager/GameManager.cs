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

    public string[] GetPlayerNames() {
        var playerNames = new string[PhotonNetwork.PlayerList.Length];
        for (var i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerNames[i] = PhotonNetwork.PlayerList[i].NickName;
        return playerNames;
    }

    public static Player[] GetPlayers() {
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
                _isPlayed[0] = true;
                LoadScene("PlaneGameScene");
                break;
            case Minigame.FruitNinja:
                _isPlayed[1] = true;
                LoadScene("Fruit Ninja");
                break;
            case Minigame.FPS:
                _isPlayed[2] = true;
                LoadScene("FPS Scene");
                break;
            case Minigame.Labyrinth:
                _isPlayed[3] = true;
                LoadScene("Labyrinth Scene");
                break;
            case Minigame.CrawlAndJump:
                _isPlayed[4] = true;
                LoadScene("Crawl and Jump");
                break;
            default:
                IsGameFinished = true;
                LoadScene("Lobby Scene");
                break;
        }
    }

    public void StartNextGame() {
        StartMinigame(GetRandomMinigame());
    }

    public void ResetMinigame() {
        for (var i = 0; i < _isPlayed.Length; i++) _isPlayed[i] = false;
    }

    public int[] GetScores() {
        var scores = new int[PhotonNetwork.PlayerList.Length];
        for (var i = 0; i < PhotonNetwork.PlayerList.Length; i++) scores[i] = PhotonNetwork.PlayerList[i].GetScore();

        return scores;
    }

    public int GetPlayerIndex(string playerName) {
        for (var i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            if (PhotonNetwork.PlayerList[i].NickName == playerName)
                return i;
        return -1;
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