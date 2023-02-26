using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIManager : MonoBehaviourPunCallbacks {
    private bool _isMenuActive;
    public GameObject playerUI;
    public GameObject playerCamera;
    public float menuLerpSpeed = 0.05f;
    public float menuDistance = 2f;
    [SerializeField] private InputActionReference openCloseInput;

    private void Start() {
        playerUI.SetActive(_isMenuActive);
    }

    private void FixedUpdate() {
        if (!_isMenuActive) return;

        var mainMenuPosition = playerUI.transform.position;
        mainMenuPosition = Vector3.Lerp(mainMenuPosition,
            playerCamera.transform.position + playerCamera.transform.forward * menuDistance, menuLerpSpeed);
        playerUI.transform.rotation = Quaternion.Lerp(playerUI.transform.rotation,
            playerCamera.transform.rotation, menuLerpSpeed);
        mainMenuPosition.y = mainMenuPosition.y < 0.5f ? 0.5f : mainMenuPosition.y;
        playerUI.transform.position = mainMenuPosition;
    }

    public void GoHome() {
        PhotonNetwork.LoadLevel("Login Scene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public override void OnEnable() {
        openCloseInput.action.performed += OpenCloseMenu;
    }

    public override void OnDisable() {
        openCloseInput.action.performed -= OpenCloseMenu;
    }

    private void OpenCloseMenu(InputAction.CallbackContext obj) {
        if (!_isMenuActive) {
            _isMenuActive = true;
            playerUI.transform.position =
                playerCamera.transform.position + playerCamera.transform.forward * menuDistance;
            var rotationVector = playerUI.transform.rotation.eulerAngles;
            rotationVector.y = playerCamera.transform.rotation.eulerAngles.y;
            playerUI.transform.rotation = Quaternion.Euler(rotationVector);
            playerUI.SetActive(true);
        }
        else {
            _isMenuActive = false;
            playerUI.SetActive(false);
        }
    }
}