using Photon.Pun;
using UnityEngine;

public class LabyrinthNetworkManager : MonoBehaviourPunCallbacks {
    [Space] [SerializeField] private Transform spawnPoint;
    [Space] [SerializeField] public GameObject roomCam;
    public static LabyrinthNetworkManager LabyrinthManager { get; private set; }
    private double _startTime;

    private void Awake() {
        if (LabyrinthManager == null) LabyrinthManager = this;
    }

    private void Start() {
        PlayerManager.MasterClient.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
            { "PickupCount", MazeRenderer.mazeRenderer.GetPickupCount() }
        });
        _startTime = PhotonNetwork.Time;
        SpawnPlayersWithDelay();
    }

    public void SpawnPlayersWithDelay() {
        PlayerManager.LocalXROrigin.transform.position = spawnPoint.position;
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        PlayerManager.LocalPlayerInstance.GetComponent<Rigidbody>().isKinematic = false;
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

    public double GetTime() {
        return PhotonNetwork.Time - _startTime;
    }
}