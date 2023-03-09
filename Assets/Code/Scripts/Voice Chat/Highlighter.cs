using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;

[RequireComponent(typeof(Canvas))]
public class Highlighter : MonoBehaviour {
    private Canvas _canvas;

    private PhotonVoiceView _photonVoiceView;

    [SerializeField] private Image recorderSprite;

    [SerializeField] private Image speakerSprite;

    private void Awake() {
        _canvas = GetComponent<Canvas>();
        if (_canvas != null && _canvas.worldCamera == null) _canvas.worldCamera = Camera.main;
        _photonVoiceView = GetComponentInParent<PhotonVoiceView>();
    }

    private void Update() {
        recorderSprite.enabled = _photonVoiceView.IsRecording;
        speakerSprite.enabled = _photonVoiceView.IsSpeaking;
    }

    private void LateUpdate() {
        if (_canvas == null || _canvas.worldCamera == null) return; // should not happen, throw error

        transform.rotation =
            Quaternion.Euler(0f, _canvas.worldCamera.transform.eulerAngles.y,
                0f); //canvas.worldCamera.transform.rotation;
    }
}