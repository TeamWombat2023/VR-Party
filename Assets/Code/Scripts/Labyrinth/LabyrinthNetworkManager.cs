using Photon.Pun;
using UnityEngine;

public class LabyrinthNetworkManager : MonoBehaviourPunCallbacks {
    [Space] [SerializeField] private Transform spawnPoint;
    [Space] [SerializeField] public GameObject roomCam;
    public static LabyrinthNetworkManager LabyrinthManager { get; private set; }

    private void Start() {
        SpawnPlayersWithDelay();
    }

    public void SpawnPlayersWithDelay() {
        PlayerManager.LocalXROrigin.transform.position = spawnPoint.position;
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }

    public void FinishGame() {
        GameManager.gameManager.OrderPlayersAndSetNewScores("Labyrinth");
        PlayerManager.OpenScoreboard();
    }
}