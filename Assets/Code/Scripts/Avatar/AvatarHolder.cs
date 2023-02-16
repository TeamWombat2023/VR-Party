using UnityEngine;

public class AvatarHolder : MonoBehaviour {

    public Transform mainAvatarTransform;
    public Transform headTransform;
    public Transform bodyTransform;
    public Transform handLeftTransform;
    public Transform handRightTransform;

    private void Start() {
        SetLayerRecursively(headTransform.gameObject, 10);
        SetLayerRecursively(bodyTransform.gameObject, 11);
    }

    private static void SetLayerRecursively(GameObject go, int layerNumber) {
        if (go == null) return;
        foreach (var trans in go.GetComponentsInChildren<Transform>(true)) {
            trans.gameObject.layer = layerNumber;
        }
    }
}