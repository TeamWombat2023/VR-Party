using UnityEngine;

public class CheckpointManager : MonoBehaviour {
    public GameObject[] Checkpoints;

    public void EnableFirstCheckPoint() {
        for (var i = 1; i < Checkpoints.Length; i++) Checkpoints[i].SetActive(false);
        Checkpoints[0].SetActive(true);
    }

    public void EnableNewCheckpoint(int index) {
        Checkpoints[index - 1].gameObject.SetActive(false);
        if (index <= Checkpoints.Length) Checkpoints[index].gameObject.SetActive(true);
    }
}
