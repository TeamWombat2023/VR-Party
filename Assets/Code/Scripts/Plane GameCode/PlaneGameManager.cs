using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaneGameManager : MonoBehaviour {
    [Header("Powerup respawn time")] public int powerupRespawnTime;

    [Header("Trigger Holders")] public GameObject checkPointsHolder;
    public Transform checkPointHolderSpawnPoint;

    public GameObject planePrefab;
    public Transform planeSpawnPoint;
    [Space] [SerializeField] public GameObject roomCam;

    public Pilot pilot;
    public float gameDuration = 105f;


    private void Start() {
        Invoke(nameof(FinishGame), gameDuration);
        SpawnPlayersWithDelay();
    }

    public void SpawnPlayersWithDelay() {
        if (PlayerManager.LocalPlayerPhotonView.IsMine) {
            PlayerManager.LocalXROrigin.transform.position = Vector3.zero;
            PlayerManager.LocalXROrigin.transform.rotation = Quaternion.Euler(0, 180, 0);
            PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousTurnProvider>().enabled = false;
            PlayerManager.LocalPlayerInstance.SetActive(false);
            var plane = PhotonNetwork.Instantiate(planePrefab.name, planeSpawnPoint.position + Vector3.left *
                GameManager.gameManager.GetPlayerIndex(PlayerManager.LocalPlayerPhotonView.Owner.NickName),
                Quaternion.identity);
            pilot.SetPlane(plane);
            PlayerManager.LocalPlayerInstance.transform.SetParent(plane.transform);
            var localCheckPoints = Instantiate(checkPointsHolder,
                checkPointHolderSpawnPoint.position, Quaternion.identity);
            Destroy(localCheckPoints, gameDuration);
            Invoke("SpawnPlayer", 5);
        }
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }

    public void FinishGame() {
        if (PlayerManager.LocalPlayerPhotonView.IsMine) {
            PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousTurnProvider>().enabled = true;
            GameManager.gameManager.OrderPlayersAndSetNewScores("Plane Game");
            PlayerManager.OpenScoreboard();
        }
    }
}