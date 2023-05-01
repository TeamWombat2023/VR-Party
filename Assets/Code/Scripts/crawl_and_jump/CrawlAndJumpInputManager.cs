using UnityEngine;

public class CrawlAndJumpInputManager : MonoBehaviour {
    private GameObject local_player;
    private Vector3 jump  = new Vector3(0.0f, 1.0f, 0.0f);
    public float jumpForce;

    private bool isGrounded;

    private void Start() {
        local_player = PlayerManager.LocalPlayerInstance;
        isGrounded = true;
    }

    private void OnCollisionStay(Collision target) {
        if(target.gameObject == local_player)
            isGrounded = true;
    }
    private void OnCollisionEnter(Collision target) {
        if(target.gameObject == local_player)
            isGrounded = true;
    }
    private void OnCollisionExit(Collision target) {
        if(target.gameObject == local_player)
            isGrounded = false;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad0)) {
            local_player.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
            //local_player.transform.GetChild(0).transform.GetChild(0).transform.position = new Vector3(0,-0.2f,0);
        }
        if (Input.GetKeyUp(KeyCode.Keypad0)) {
            local_player.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
            //local_player.transform.GetChild(0).transform.GetChild(0).transform.position = new Vector3(0,0,0);;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) && isGrounded) {
            local_player.GetComponent<Rigidbody>().AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyUp(KeyCode.Keypad1)){
            isGrounded = false;
        }
    }
}