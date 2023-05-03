using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class rotator_powerup : MonoBehaviour {
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

                if (other.transform.parent.parent == PlayerManager.LocalAvatar.transform &&
                    LabyrinthNetworkManager.LabyrinthManager.GetTime() > 5)
                    PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed=3;
                    //start timer for 10 seconds
                    Invoke("resetSpeed", 3);
                }
        }
    }

    private void resetSpeed(){
        PlayerManager.LocalXROrigin.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed=1;
    }

}