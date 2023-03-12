using UnityEngine;
using UnityEngine.InputSystem;

public class TemporaryController : MonoBehaviour {
    public GameObject player;
    private Vector3 jump;
    public float jumpForce;

    private bool isGrounded;
    private bool isCrawling;

    public GameObject gamePlane;

    public InputActionReference jumpInput;
    public InputActionReference crawlInput;

    private void Start() {
        jump = new Vector3(0.0f, 1.0f, 0.0f);
    }

    private void OnCollisionStay(Collision target) {
        if (target.transform.name == gamePlane.transform.name) isGrounded = true;
    }

    private void OnEnable() {
        jumpInput.action.performed += Jump;
        crawlInput.action.performed += Crawl;
    }

    private void OnDisable() {
        crawlInput.action.performed -= Crawl;
    }

    private void Jump(InputAction.CallbackContext context) {
        if (isGrounded) {
            player.GetComponent<Rigidbody>().AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void Crawl(InputAction.CallbackContext context) {
        if (!isCrawling) {
            player.GetComponent<CapsuleCollider>().height = 1.5f;
            player.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.75f, 0);
            isCrawling = true;
        }
        else {
            player.GetComponent<CapsuleCollider>().height = 2;
            player.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
            isCrawling = false;
        }
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad0)) {
            player.GetComponent<CapsuleCollider>().height = 1.5f;
            player.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.75f, 0);
        }

        if (Input.GetKeyUp(KeyCode.Keypad0)) {
            player.GetComponent<CapsuleCollider>().height = 2;
            player.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) & isGrounded) {
            player.GetComponent<Rigidbody>().AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyUp(KeyCode.Keypad1)) isGrounded = false;
    }
}