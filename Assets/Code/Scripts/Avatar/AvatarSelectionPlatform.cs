using System.Collections.Generic;
using UnityEngine;

public class AvatarSelectionPlatform : MonoBehaviour {
    public List<GameObject> avatars = new();
    public int currentAvatar = 0;

    public AvatarSelectionManager avatarSelectionManager;

    private void Start() {
        foreach (var avatar in avatars) avatar.SetActive(false);
        avatars[currentAvatar].SetActive(true);
    }

    public void NextAvatar() {
        avatars[currentAvatar].SetActive(false);
        currentAvatar = (currentAvatar + 1) % avatars.Count;
        avatars[currentAvatar].SetActive(true);
    }

    public void PreviousAvatar() {
        avatars[currentAvatar].SetActive(false);
        currentAvatar = (currentAvatar - 1) % avatars.Count;
        if (currentAvatar < 0) currentAvatar = avatars.Count - 1;
        avatars[currentAvatar].SetActive(true);
    }

    public void SelectAvatar() {
        avatarSelectionManager.SelectAvatar(currentAvatar);
    }
}