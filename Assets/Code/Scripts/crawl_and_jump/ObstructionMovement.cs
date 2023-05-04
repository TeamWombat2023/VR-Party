using UnityEngine;
using Photon.Pun;

public class ObstructionMovement : MonoBehaviour {
    // Start is called before the first frame update
    public Rigidbody rb;
    public int isRotating = -1;
    public int speed = 7;

    private void Start() {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezeRotationY;
    }

    private void FixedUpdate() {
        if (PhotonNetwork.IsMasterClient) {
            rb.velocity = new Vector3(-speed, 0, 0);
            if (isRotating < 0) transform.Rotate(0f, 0f, 1f, Space.Self);
            if (isRotating > 0) transform.Rotate(0f, 0f, -1f, Space.Self);
            if (transform.position.x < -50) PhotonNetwork.Destroy(gameObject);
        }
    }
}