using UnityEngine;
using TMPro;
using Photon.Voice.PUN;

public class VoiceDebugUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI voiceState;
    [SerializeField] private GameObject micImage;

    private PunVoiceClient _punVoiceNetwork;
    private bool _isPunVoiceNetworkNull;

    private void Start() {
        _isPunVoiceNetworkNull = _punVoiceNetwork == null;
    }

    private void Awake() {
        _punVoiceNetwork = PunVoiceClient.Instance;
    }

    private void OnEnable() {
        _punVoiceNetwork.Client.StateChanged += VoiceClientStateChanged;
    }

    private void OnDisable() {
        _punVoiceNetwork.Client.StateChanged -= VoiceClientStateChanged;
    }

    private void Update() {
        if (_isPunVoiceNetworkNull) _punVoiceNetwork = PunVoiceClient.Instance;
    }


    private void VoiceClientStateChanged(Photon.Realtime.ClientState fromState, Photon.Realtime.ClientState toState) {
        UpdateUiBasedOnVoiceState(toState);
    }

    private void UpdateUiBasedOnVoiceState(Photon.Realtime.ClientState voiceClientState) {
        voiceState.text = $"PhotonVoice: {voiceClientState}";
        if (voiceClientState != Photon.Realtime.ClientState.Joined) return;
        voiceState.gameObject.SetActive(false);
        micImage.SetActive(false);
    }
}