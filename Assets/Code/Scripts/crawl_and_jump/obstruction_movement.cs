using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public bool isRotating = false;
    void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(-Vector3.right * Time.deltaTime * 10);
        //rb.AddForce(2000 * Time.deltaTime, 0, 0);
        rb.velocity = new Vector3(-10, 0, 0);
        if(isRotating){
            transform.Rotate(0f, 1f, 0f, Space.Self);
        }
    }
}
