using Photon.Pun;
using UnityEngine;

public class CrawlAndJumpKillCondition : MonoBehaviour {
    public Vector3 teleport_transform;
    public GameObject kaybedenler_klubu;

    private void Start() {
        teleport_transform = kaybedenler_klubu.transform.position + new Vector3(0, 5, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Body") && other.gameObject.transform.parent.parent.parent.gameObject ==
            PlayerManager.LocalPlayerInstance) {
            PlayerManager.LocalXROrigin.transform.position = teleport_transform;
            PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
            PlayerManager.LocalPlayerInstance.GetComponent<Rigidbody>().isKinematic = true;

            // set custom property if the player fell and hold its timestamp
            PlayerManager.LocalPlayerPhotonView.Owner.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
                { "CrawlAndJump", PhotonNetwork.Time }
            });
        }
    }
}