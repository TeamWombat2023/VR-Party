using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviour {
    public GameObject startButtonHolder;
    public TMP_Text debugText;


    private Animator _startButtonHolderAnimator;
    private string currentState;
    private const string BUTTON_IDLE = "idle";
    private const string BUTTON_PRESSED = "button_pressed";

    private void Start() {
        _startButtonHolderAnimator = startButtonHolder.GetComponent<Animator>();
    }


    public void StartButtonPressed() {
        debugText.text = "Buttona basildi.";
        AnimateStartButton();
    }


    public void StartButtonSelected() {
        debugText.text = "Buttona select yapildi. Simple";
        AnimateStartButton();
        debugText.text += "debug calisti";
    }


    private void AnimateStartButton() {
        ChangeAnimationState(BUTTON_PRESSED);
        ChangeAnimationState(BUTTON_IDLE);
    }

    private void ChangeAnimationState(string newState) {
        if (currentState == newState) currentState = BUTTON_IDLE;
        _startButtonHolderAnimator.Play(newState);
        currentState = newState;
    }
}