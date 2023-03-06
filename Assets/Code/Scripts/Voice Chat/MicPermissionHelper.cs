using UnityEngine;

#if PLATFORM_ANDROID
using UnityEngine.Android;

#endif

public class MicPermissionHelper : MonoBehaviour {
    //GameObject dialog = null;

    // Start is called before the first frame update
    private void Start() {
#if PLATFORM_ANDROID

        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);
        //dialog = new GameObject(); } #endif Debug.Log(Microphone.devices.ToString()); }
#endif
    }
}