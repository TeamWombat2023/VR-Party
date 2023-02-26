using UnityEngine;

public class AvatarInputConverter : MonoBehaviour {
    //Avatar Transforms
    [Header("Avatar Transforms")] public Transform mainAvatarTransform;
    public Transform avatarHead;
    public Transform avatarBody;
    public Transform avatarLeftHand;
    public Transform avatarRightHand;

    //XROrigin Transforms
    [Header("XROrigin Transforms")] public Transform mainCamera;
    public Transform leftHandController;
    public Transform rightHandController;

    public Vector3 headPositionOffset;

    private void Update() {
        mainAvatarTransform.position =
            Vector3.Lerp(mainAvatarTransform.position, mainCamera.position + headPositionOffset, 0.5f);
        avatarHead.rotation = Quaternion.Lerp(avatarHead.rotation, mainCamera.rotation, 0.5f);
        avatarBody.rotation = Quaternion.Lerp(avatarBody.rotation,
            Quaternion.Euler(new Vector3(0, avatarHead.rotation.eulerAngles.y, 0)), 0.05f);

        avatarRightHand.position = Vector3.Lerp(avatarRightHand.position, rightHandController.position, 0.5f);
        avatarRightHand.rotation = Quaternion.Lerp(avatarRightHand.rotation, rightHandController.rotation, 0.5f);

        avatarLeftHand.position = Vector3.Lerp(avatarLeftHand.position, leftHandController.position, 0.5f);
        avatarLeftHand.rotation = Quaternion.Lerp(avatarLeftHand.rotation, leftHandController.rotation, 0.5f);
    }
}