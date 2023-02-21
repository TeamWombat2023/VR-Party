using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class AvatarSelectionPlatform : MonoBehaviour {
    
    public List<GameObject> avatars = new List<GameObject>();
    public int currentAvatar = 0;
    private void Start() {
        foreach (var avatar in avatars) {
            avatar.SetActive(false);
        }
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
        if (currentAvatar < 0) {
            currentAvatar = avatars.Count - 1;
        }
        avatars[currentAvatar].SetActive(true);
    }
}
