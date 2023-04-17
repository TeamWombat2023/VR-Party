using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks {
    public static GameManager gameManager { get; private set; }
    [SerializeField] private GameObject playerPrefab;

    public List<GameObject> players = new();
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
        players.Add(player);
        _playerCount++;
        return player;
    }

    public void RemovePlayer(GameObject player) {
        players.Remove(player);
        _playerCount--;
    }

    // get player by name
    public GameObject GetPlayer(string playerName) {
        foreach (var player in players)
            if (player.GetComponent<PhotonView>().Owner.NickName == playerName)
                return player;
        return null;
    }

    public Minigame GetRandomMinigame() {
        var random = Random.Range(0, 5);
        while (_isPlayed[random]) random = Random.Range(0, 5);
        _isPlayed[random] = true;
        return (Minigame)random;
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
                throw new ArgumentOutOfRangeException(nameof(minigame), minigame, null);
        }
    }


    public enum Minigame {
        PlaneGame,
        FruitNinja,
        FPS,
        Labyrinth,
        CrawlAndJump
    }
}