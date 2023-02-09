using TMPro;
using UnityEngine;

public class Keyboard : MonoBehaviour {
    
    public TMP_InputField inputField;
    public TypingArea typingArea;
    public GameObject letters;
    public bool caps = true;
    
    public void InsertChar(string c) {
        if (inputField == null) return;
        inputField.text += c;
    }
    
    public void DeleteChar() {
        if (inputField != null && inputField.text.Length > 0) {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
    
    public void InsertSpace() {
        if (inputField == null) return;
        inputField.text += " ";
    } 
    
    public void CapsLock() {
        caps = !caps;
        var buttons = letters.GetComponentsInChildren<KeyboardButton>();
        foreach (var button in buttons) {
            button.GameToButtonText();
        }
    }

    public void Clear() {
        if (inputField == null) return;
        inputField.text = "";
    }

    public void Enter() {
        inputField = null;
    }
}
