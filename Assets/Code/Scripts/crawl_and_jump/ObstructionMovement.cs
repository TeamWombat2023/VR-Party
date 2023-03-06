using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(-Vector3.right * Time.deltaTime * 10);
        //rb.AddForce(2000 * Time.deltaTime, 0, 0);
        //1rb.velocity = new Vector3(10, 0, 0);
    }
}
