using UnityEngine;
using UnityEngine.InputSystem;

public class CrawlAndJumpInputManager : MonoBehaviour {
    public float jumpForce;
    public InputActionReference jumpInput;
    public InputActionReference crawlInput;
    private GameObject local_player;
    private Rigidbody _rigidbody;

    private bool isGrounded;
    private bool isCrawling;

    private void Start() {
        local_player = PlayerManager.LocalPlayerInstance;
        isGrounded = true;
        _rigidbody = local_player.GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision target) {
        if (target.gameObject == local_player)
            isGrounded = true;
    }

    private void OnCollisionEnter(Collision target) {
        if (target.gameObject == local_player)
            isGrounded = true;
    }

    private void OnCollisionExit(Collision target) {
        if (target.gameObject == local_player)
            isGrounded = false;
    }

    private void OnEnable() {
        jumpInput.action.performed += Jump;
        crawlInput.action.performed += Crawling;
    }

    private void OnDisable() {
        crawlInput.action.performed -= Crawling;
    }

    private void Jump(InputAction.CallbackContext obj) {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void Crawling(InputAction.CallbackContext obj) {
        if (isCrawling) {
            // local_player.transform.localScale = new Vector3(1, 1, 1);
            // playeri kaldir
        }
        else {
            // local_player.transform.localScale = new Vector3(1, 0.5f, 1);
            // playeri yere indir
        }
    }
}