using UnityEngine;
using Photon.Pun;

public class obstruction_spawner : MonoBehaviourPunCallbacks {
    private double _obstructionSpawnTimer = 0f;

    private void Start() {
        InvokeRepeating("obstruction_spawn", 10f, 3f);
        _obstructionSpawnTimer = PhotonNetwork.Time;
    }

    private void obstruction_spawn() {
        if (PhotonNetwork.IsMasterClient) {
            if (!PhotonNetwork.MasterClient.CustomProperties.ContainsKey("PlayerFellCount")) {
                PhotonNetwork.MasterClient.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
                    { "PlayerFellCount", 0 }
                });
            }
            else {
                if ((int)PhotonNetwork.MasterClient.CustomProperties["PlayerFellCount"] <=
                    PhotonNetwork.PlayerList.Length) {
                    var clone = PhotonNetwork.Instantiate("Obstruction", transform.position,
                        Quaternion.Euler(90, 0, 0));
                    var topOrBottom = Random.Range(0, 2);
                    var randomRotation = Random.Range(0, 3);
                    clone.GetComponent<ObstructionMovement>().isRotating = randomRotation;
                    clone.GetComponent<ObstructionMovement>().speed =
                        7 + (int)(PhotonNetwork.Time - _obstructionSpawnTimer) / 20;
                    if (topOrBottom == 0) clone.transform.position += new Vector3(0, 0.8f, 0);
                }
            }
        }
    }
}