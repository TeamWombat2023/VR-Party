using Photon.Pun;
using UnityEngine;

public class Rotator : MonoBehaviour {
    private void Update() {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Head") || other.gameObject.CompareTag("Body"))
            if (GameManager.gameManager.GetCurrentSceneName() == "Labyrinth Scene") {
                if (PlayerManager.MasterClient != null) {
                    if (PlayerManager.MasterClient.CustomProperties.ContainsKey("PickupCount")) {
                        if ((int)PlayerManager.MasterClient.CustomProperties["PickupCount"] <= 0)
                            LabyrinthNetworkManager.LabyrinthManager.FinishGame();
                        else
                            PlayerManager.MasterClient.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
                                { "PickupCount", (int)PlayerManager.MasterClient.CustomProperties["PickupCount"] - 1 }
                            });
                    }
                    else {
                        PlayerManager.MasterClient.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
                            { "PickupCount", MazeRenderer.mazeRenderer.GetPickupCount() }
                        });
                    }
                }

                if (other.transform.parent.parent == PlayerManager.LocalAvatar.transform &&
                    LabyrinthNetworkManager.LabyrinthManager.GetTime() > 5)
                    PlayerManager.AddScoreToMiniGame("Labyrinth", 10);
                PhotonNetwork.Destroy(gameObject);
            }
    }
}