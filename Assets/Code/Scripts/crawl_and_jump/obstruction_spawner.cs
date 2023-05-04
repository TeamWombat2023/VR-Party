using UnityEngine;
using Photon.Pun;

public class obstruction_spawner : MonoBehaviourPunCallbacks {
    private float top_or_bottom = 0f;

    private void Start() {
        InvokeRepeating("obstruction_spawn", 10f, 3f);
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
                    top_or_bottom = Random.Range(0, 2);
                    if (Random.Range(0, 2) == 0) clone.GetComponent<ObstructionMovement>().isRotating = true;
                    if (top_or_bottom == 0) clone.transform.position += new Vector3(0, 0.8f, 0);
                }
            }
        }
    }
}