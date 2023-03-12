using Photon.Pun;
using UnityEngine;

public class MultiplayerGameManager : MonoBehaviour {
    public void LoadMinigame(string minigameName) {
        Debug.Log("Loading minigame: " + minigameName);
        PhotonNetwork.LoadLevel(minigameName);
    }
}