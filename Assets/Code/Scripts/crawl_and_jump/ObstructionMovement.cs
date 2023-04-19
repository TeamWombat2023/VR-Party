using UnityEngine;
using Photon.Pun;
public class ObstructionMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public bool isRotating = false;
    void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }
    void FixedUpdate()
    {
        if(PhotonNetwork.IsMasterClient){
            rb.velocity = new Vector3(-10, 0, 0);
            if(isRotating){
                transform.Rotate(0f, 1f, 0f, Space.Self);
            }
            if(transform.position.x < -50){
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
