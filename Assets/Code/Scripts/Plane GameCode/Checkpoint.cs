using Code.Scripts.Plane_GameCode;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public int CheckpointNumber;
    public CheckpointManager Manager;

    private void Start() {
        Manager = FindObjectOfType<CheckpointManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Plane") &&
            other.transform.Find("Generic Player").gameObject == PlayerManager.LocalPlayerInstance) {
            PlayerManager.AddScoreToMiniGame("Plane Game", 100);
            Manager.EnableNewCheckpoint(CheckpointNumber + 1);
        }
    }
}