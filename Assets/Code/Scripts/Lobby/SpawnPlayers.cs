using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float startX;
    public float startY;
    public float startZ;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPosition = new Vector3(startX, startY, startZ);
        PhotonNetwork.Instantiate(playerPrefab.name, startPosition, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
