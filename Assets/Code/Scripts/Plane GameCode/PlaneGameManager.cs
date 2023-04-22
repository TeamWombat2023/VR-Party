using System.Collections;
using Code.Scripts.Plane_GameCode;
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

    private GameObject localPlane;


    private void Start() {
        Invoke(nameof(FinishGame), gameDuration);
        SpawnPlayersWithDelay();
    }

    public void SpawnPlayersWithDelay() {
        if (PlayerManager.LocalPlayerPhotonView.IsMine) {
            PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousTurnProvider>().enabled = false;
            PlayerManager.LocalPlayerInstance.SetActive(false);
            localPlane = PhotonNetwork.Instantiate(planePrefab.name, planeSpawnPoint.position + Vector3.left *
                GameManager.gameManager.GetPlayerIndex(PlayerManager.LocalPlayerPhotonView.Owner.NickName),
                Quaternion.identity);
            pilot.SetPlane(localPlane);
            localPlane.GetComponent<Rigidbody>().isKinematic = true;
            PlayerManager.LocalPlayerInstance.transform.SetParent(localPlane.transform);
            PlayerManager.LocalPlayerInstance.transform.localPosition = Vector3.zero + Vector3.up;
            PlayerManager.LocalPlayerInstance.transform.localRotation = Quaternion.Euler(0, 180, 0);
            var localCheckPoints = Instantiate(checkPointsHolder,
                checkPointHolderSpawnPoint.position, Quaternion.identity);
            localCheckPoints.GetComponent<CheckpointManager>().EnableFirstCheckPoint();
            Destroy(localCheckPoints, gameDuration);
            Invoke("SpawnPlayer", 5);
            Invoke("ActivatePlane", 10);
        }
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }

    public void ActivatePlane() {
        localPlane.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void FinishGame() {
        if (PlayerManager.LocalPlayerPhotonView.IsMine) {
            PlayerManager.LocalPlayerInstance.transform.SetParent(null);
            PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousTurnProvider>().enabled = true;
            GameManager.gameManager.OrderPlayersAndSetNewScores("Plane Game");
            PlayerManager.OpenScoreboard();
        }
    }
}