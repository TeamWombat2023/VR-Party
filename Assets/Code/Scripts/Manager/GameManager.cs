using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks {
    public static GameManager gameManager { get; private set; }
    [SerializeField] private GameObject playerPrefab;

    private NetworkManager _networkManager;
    public List<GameObject> players = new();

    private void Awake() {
        if (gameManager == null) {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public string GetCurrentSceneName() {
        var scene = SceneManager.GetActiveScene();
        return scene.name;
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public GameObject CreatePlayer() {
        var player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        players.Add(player);
        return player;
    }


    public enum Minigame {
        PlaneGame,
        FruitNinja,
        FPS,
        Labyrinth,
        CrawlAndJump
    }
}