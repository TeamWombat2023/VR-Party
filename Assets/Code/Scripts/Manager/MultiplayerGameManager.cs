using Photon.Pun;
using UnityEngine;

public class MultiplayerGameManager : MonoBehaviour {
    public void LoadMinigame(string minigameName) {
        GameManager.gameManager.LoadScene(minigameName);
    }
}