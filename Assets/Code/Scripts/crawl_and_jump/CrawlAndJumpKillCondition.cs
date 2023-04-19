using UnityEngine;

public class CrawlAndJumpKillCondition : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Body") && other.gameObject.transform.parent.parent.parent.gameObject ==
            PlayerManager.LocalPlayerInstance)
            PlayerManager.LocalXROrigin.transform.position = Vector3.zero;
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
    }
}