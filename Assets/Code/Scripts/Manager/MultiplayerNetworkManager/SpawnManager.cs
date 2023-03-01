using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private GameObject GenericVRPlayerPrefab;

    public Vector3 spawnPosition;

    private void Start() {
        Invoke(nameof(SpawnPlayer), 0.5f);
    }

    private void SpawnPlayer() {
        PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
    }
}