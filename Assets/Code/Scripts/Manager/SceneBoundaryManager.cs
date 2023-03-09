using UnityEngine;

public class SceneBoundaryManager : MonoBehaviour {
    [SerializeField] private GameObject player;

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == player) Invoke(nameof(SpawnPlayer), 3f);
    }

    private void SpawnPlayer() {
        player.transform.position = new Vector3(0, 0, 0);
    }
}