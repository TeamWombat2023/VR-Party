using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Vector3 jump;
    public float jumpForce;
    
    private bool isGrounded;

    public GameObject gamePlane;

    void Start()
    {
        jump = new Vector3(0.0f, 1.0f, 0.0f);
    }

    void OnCollisionStay(Collision target){
        if(target.transform.name == gamePlane.transform.name){
            isGrounded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad0)){
            player.GetComponent<CapsuleCollider>().height = 2;
            player.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
        }
        if(Input.GetKeyUp(KeyCode.Keypad0)){
            player.GetComponent<CapsuleCollider>().height = 4;
            player.GetComponent<CapsuleCollider>().center = new Vector3(0, 2, 0);
        }
        if(Input.GetKeyDown(KeyCode.Keypad1) & isGrounded){
            player.GetComponent<Rigidbody>().AddForce(jump * jumpForce, ForceMode.Impulse);
        }

    }
}
