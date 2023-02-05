using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            Invoke("SpawnPlayer", 3f);
        }
    }
    private void SpawnPlayer() {
        player.transform.position = new Vector3(0, 0, 0);
    }
}
