using UnityEngine;

public class FruitNinjaManager : MonoBehaviour {
    [Space] [SerializeField] public GameObject roomCam;

    private void Start() {
        SpawnPlayersWithDelay();
    }

    private void SpawnPlayersWithDelay() {
        PlayerManager.ActivateHands("Fruit Ninja");
        PlayerManager.LocalXROrigin.transform.position = Vector3.zero;
        PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        PlayerManager.LocalPlayerInstance.SetActive(false);
        Invoke("SpawnPlayer", 5);
    }

    public void SpawnPlayer() {
        PlayerManager.LocalPlayerInstance.SetActive(true);
        roomCam.SetActive(false);
    }
}