using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour{
    
    [SerializeField]
    private GameObject genericPlayerPrefab;
    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    [SerializeField]
    private GameObject xrOrigin;
    private void Start() {
        Invoke("SpawnPlayer", 1);
    }
    private void SpawnPlayer() {
        xrOrigin.SetActive(false);
        genericPlayerPrefab = PhotonNetwork.Instantiate(genericPlayerPrefab.name, spawnPosition, Quaternion.identity);
    }
}
