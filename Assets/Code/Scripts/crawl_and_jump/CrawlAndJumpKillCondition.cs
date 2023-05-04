using Photon.Pun;
using UnityEngine;

public class CrawlAndJumpKillCondition : MonoBehaviour {
    public Vector3 teleport_transform;
    public GameObject kaybedenler_klubu;

    private void Start() {
        teleport_transform = kaybedenler_klubu.transform.position + new Vector3(0, 5, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("LocalAvatarHead") ||
            other.gameObject.layer == LayerMask.NameToLayer("LocalAvatarHead")) {
            PlayerManager.LocalXROrigin.transform.position = teleport_transform;
            PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;

            // set custom property if the player fell and hold its timestamp
            if (!(bool)PlayerManager.LocalPlayerPhotonView.Owner.CustomProperties["HasTimeSet"]) {
                PlayerManager.LocalPlayerPhotonView.Owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
                    { "CrawlAndJump", PhotonNetwork.Time }
                });
                PlayerManager.LocalPlayerPhotonView.Owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
                    { "HasTimeSet", true }
                });
            }
        }

        if ((other.gameObject.CompareTag("Body") || other.gameObject.CompareTag("Head")) &&
            !(bool)other.gameObject.transform.parent.parent.parent.gameObject.GetComponent<PhotonView>().Owner
                .CustomProperties["HasTimeSet"])
            UpdateFellDownCount();
    }

    private void UpdateFellDownCount() {
        if (PlayerManager.MasterClient.CustomProperties.ContainsKey("PlayerFellCount")) {
            PlayerManager.MasterClient.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
                { "PlayerFellCount", (int)PlayerManager.MasterClient.CustomProperties["PlayerFellCount"] + 1 }
            });
            if ((int)PlayerManager.MasterClient.CustomProperties["PlayerFellCount"] >=
                PhotonNetwork.PlayerList.Length)
                CrawlAndJumpManager.FinishGame();
        }
    }
}