using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneGameManager : MonoBehaviour {
    [Header("Powerup respawn time")] public int powerupRespawnTime;

    [Header("Trigger Holders")] public GameObject checkPointsHolder;
    public GameObject powerupHolder;

    public GameObject planePrefab;
    public Transform planeSpawnPoint;
    [Space] [SerializeField] public GameObject roomCam;

    public Pilot pilot;
    private Checkpoint[] _checkpoints;
    private int _currentCheckpoint;

    private Powerup[] _powerups;


    // Start is called before the first frame update
    private void Start() {
        _checkpoints = checkPointsHolder.GetComponentsInChildren<Checkpoint>();
        _currentCheckpoint = 0;

        _powerups = powerupHolder.GetComponentsInChildren<Powerup>();
        EnableNewCheckpoint();

        StartCoroutine(GameEndEvent());
        SpawnPlayersWithDelay();
    }

    public void SpawnPlayersWithDelay() {
        PlayerManager.LocalXROrigin.transform.position = Vector3.zero;
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        PlayerManager.LocalPlayerInstance.SetActive(false);
        var plane = PhotonNetwork.Instantiate(planePrefab.name, planeSpawnPoint.position, Quaternion.identity);
        pilot.SetPlane(plane);
        PlayerManager.LocalPlayerInstance.transform.SetParent(plane.transform);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }

    public IEnumerator GameEndEvent() {
        yield return new WaitForSeconds(25);
        Debug.Log("Game has 75 seconds.");
        yield return new WaitForSeconds(25);
        Debug.Log("Game has 50 seconds.");
        yield return new WaitForSeconds(25);
        Debug.Log("Game has 25 seconds.");
        yield return new WaitForSeconds(25);
        Debug.Log("Game has 0 seconds.");
    }


    public void EnableNewCheckpoint() {
        foreach (var checkpoint in _checkpoints) checkpoint.gameObject.SetActive(false);

        _checkpoints[_currentCheckpoint++].gameObject.SetActive(true);
    }


    public void StartPowerupRespawnTimer() {
    }
}