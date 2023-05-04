using UnityEngine;
using UnityEngine.InputSystem;

public class CrawlAndJumpInputManager : MonoBehaviour {
    public float jumpForce;
    public InputActionReference jumpInput;
    public InputActionReference crawlInput;
    private GameObject local_player;
    private Rigidbody _rigidbody;

    private MeshCollider local_player_mesh_collider;

    private bool isGrounded;
    private bool isCrawling;

    private void Start() {
        local_player = PlayerManager.LocalPlayerInstance;
        isGrounded = true;
        isCrawling = false;
        _rigidbody = local_player.GetComponent<Rigidbody>();
        local_player_mesh_collider = local_player.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).GetComponent<MeshCollider>();
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
        Debug.Log("i be jumpin");
        if(isGrounded && !isCrawling){
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        isGrounded = false;
    }

    private void Crawling(InputAction.CallbackContext obj) {
        Debug.Log("i be crawlin");
        if (isCrawling) {
            local_player.transform.position = local_player.transform.position + new Vector3(0, 1.3f, 0);
            local_player_mesh_collider.enabled = true;
            isCrawling = false;
        }
        else {
            local_player_mesh_collider.enabled = false;
            isCrawling = true;
            // local_player.transform.localScale = new Vector3(1, 0.5f, 1);
            // playeri yere indir
        }
    }
}