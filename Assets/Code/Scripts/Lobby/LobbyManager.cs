using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public GameObject startButtonHolder;
    private Animator _startButtonHolderAnimator;
    public TMP_Text debugText;

    private string START_BUTTON_ANIMATION = "isStartActivated";
    // Start is called before the first frame update
    void Start()
    {
        _startButtonHolderAnimator = startButtonHolder.GetComponent<Animator>();
    }


    public void StartButtonPressed()
    {
        debugText.text = "Buttona basildi.";
        AnimateStartButton();
    }

    
    public void StartButtonSelected()
    {
        debugText.text = "Buttona select yapildi. Simple";
        AnimateStartButton();
        debugText.text += "debug calisti";
    }
    
    
    public void AnimateStartButton()
    {
        
        //_startButtonHolderAnimator.SetBool(START_BUTTON_ANIMATION, true);
        _startButtonHolderAnimator.SetTrigger(START_BUTTON_ANIMATION);
        //debugText.text = "Animation calisti.... Value:" + _startButtonHolderAnimator.parameters[0];
        //_startButtonHolderAnimator.SetBool(START_BUTTON_ANIMATION, false);
        //Invoke(nameof(ResetButton),0.1f);
    }

    private void ResetButton()
    {
        _startButtonHolderAnimator.SetBool(START_BUTTON_ANIMATION, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
