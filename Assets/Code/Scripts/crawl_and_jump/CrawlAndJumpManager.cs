using UnityEngine;

public class CrawlAndJumpManager : MonoBehaviour {
    [Space] [SerializeField] public GameObject roomCam;

    private void Start() {
        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("GenericPlayerModifier", 5);
    }

    private void GenericPlayerModifier() {
        roomCam.SetActive(false);
        PlayerManager.LocalXROrigin.GetComponent<CharacterController>().enabled = false;
        PlayerManager.LocalPlayerInstance.SetActive(true);
    }
}