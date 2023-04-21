using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIManager : MonoBehaviourPunCallbacks {
    private bool _isUIMenuActive;
    private bool _isScoreBoardActive;
    public GameObject playerUI;
    public GameObject scoreBoardUI;
    public ScoreBoardElement scoreBoardElement;
    public Transform scoreBoardContent;
    public GameObject playerCamera;
    public float menuLerpSpeed = 0.05f;
    public float menuDistance = 2f;
    [SerializeField] private InputActionReference openCloseInput;


    private void Start() {
        scoreBoardUI.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(PhotonNetwork.IsMasterClient);
        scoreBoardUI.SetActive(_isScoreBoardActive);
        playerUI.SetActive(_isUIMenuActive);
    }

    private void FixedUpdate() {
        if (_isUIMenuActive) {
            var mainMenuPosition = playerUI.transform.position;
            mainMenuPosition = Vector3.Lerp(mainMenuPosition,
                playerCamera.transform.position + playerCamera.transform.forward * menuDistance, menuLerpSpeed);
            playerUI.transform.rotation = Quaternion.Lerp(playerUI.transform.rotation,
                playerCamera.transform.rotation, menuLerpSpeed);
            mainMenuPosition.y = mainMenuPosition.y < 0.5f ? 0.5f : mainMenuPosition.y;
            playerUI.transform.position = mainMenuPosition;
        }

        if (_isScoreBoardActive) {
            var scoreBoardPosition = scoreBoardUI.transform.position;
            scoreBoardPosition = Vector3.Lerp(scoreBoardPosition,
                playerCamera.transform.position + playerCamera.transform.forward * menuDistance, menuLerpSpeed);
            scoreBoardUI.transform.rotation = Quaternion.Lerp(scoreBoardUI.transform.rotation,
                playerCamera.transform.rotation, menuLerpSpeed);
            scoreBoardPosition.y = scoreBoardPosition.y < 0.5f ? 0.5f : scoreBoardPosition.y;
            scoreBoardUI.transform.position = scoreBoardPosition;
        }
    }

    public void OpenScoreBoard() {
        var scores = GameManager.gameManager.GetScores();
        var playerNames = GameManager.gameManager.GetPlayerNames();

        for (var i = 0; i < scores.Length; i++)
        for (var j = 0; j < scores.Length - 1; j++)
            if (scores[j] < scores[j + 1]) {
                (scores[j], scores[j + 1]) = (scores[j + 1], scores[j]);
                (playerNames[j], playerNames[j + 1]) = (playerNames[j + 1], playerNames[j]);
            }

        for (var i = 0; i < scores.Length; i++) {
            var scoreBoardElementInstance = Instantiate(scoreBoardElement, scoreBoardContent);
            scoreBoardElementInstance.SetScoreInfo(i + 1, playerNames[i], scores[i]);
        }

        _isScoreBoardActive = true;
        scoreBoardUI.SetActive(_isScoreBoardActive);
    }

    public void CloseScoreBoard() {
        _isScoreBoardActive = false;
        scoreBoardUI.SetActive(_isScoreBoardActive);
        foreach (Transform child in scoreBoardUI.transform) Destroy(child.gameObject);
    }

    public void StartNextGame() {
        CloseScoreBoard();
        GameManager.gameManager.StartNextGame();
    }

    public void GoHome() {
        PhotonNetwork.LeaveRoom();
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
        if (!_isUIMenuActive) {
            _isUIMenuActive = true;
            playerUI.transform.position =
                playerCamera.transform.position + playerCamera.transform.forward * menuDistance;
            var rotationVector = playerUI.transform.rotation.eulerAngles;
            rotationVector.y = playerCamera.transform.rotation.eulerAngles.y;
            playerUI.transform.rotation = Quaternion.Euler(rotationVector);
            playerUI.SetActive(true);
        }
        else {
            _isUIMenuActive = false;
            playerUI.SetActive(false);
        }
    }
}