using UnityEngine;
using Photon.Pun;

public class obstruction_spawner : MonoBehaviourPunCallbacks {
    public GameObject obstructionPrefab;

    private float top_or_bottom = 0f;
    private void Start() {
        InvokeRepeating("obstruction_spawn", 5f, 3f);
    }

    private void obstruction_spawn() {
        if (PhotonNetwork.IsMasterClient) {
            GameObject clone = PhotonNetwork.Instantiate("Obstruction", transform.position, Quaternion.identity);
            top_or_bottom = Random.Range(0, 2);
            if (Random.Range(0, 2) == 0) clone.GetComponent<ObstructionMovement>().isRotating = true;
            if (top_or_bottom == 0) clone.transform.position = clone.transform.position + new Vector3(0, 0.8f, 0);
        }
    }
}