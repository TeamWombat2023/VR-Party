using UnityEngine;

public class Pickup : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.tag);
    }
}