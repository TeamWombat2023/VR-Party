using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;
using Keyboard = VRKeys.Keyboard;

public class VRKeyboardManager : MonoBehaviour
{
    public Vector3 relativePosition = new Vector3(0f, 0f, 2f);
    public GameObject playerCamera;
    public Keyboard keyboard;
    public TMP_InputField inputField;
    public GameObject leftHandController;
    public GameObject rightHandController;
    public GameObject leftMallet;
    public GameObject rightMallet;

    public void EnableVRKeyboard()
	{		
		keyboard.Enable();
		keyboard.canvas.gameObject.SetActive(false);
		keyboard.OnUpdate.AddListener(HandleUpdate);
		keyboard.OnSubmit.AddListener(HandleSubmit);
		keyboard.OnCancel.AddListener(HandleCancel);

		keyboard.gameObject.transform.position = playerCamera.transform.position + relativePosition;
		AttachMallets();

		leftHandController.GetComponent<XRRayInteractor>().enabled = false;
		rightHandController.GetComponent<XRRayInteractor>().enabled = false;
	}

	void AttachMallets()
	{
		leftMallet.transform.SetParent(leftHandController.transform);
		leftMallet.transform.localPosition = Vector3.zero;
		leftMallet.transform.localRotation = Quaternion.Euler(new Vector3(90f,0f,0f));
		leftMallet.SetActive(true);

		rightMallet.transform.SetParent(rightHandController.transform);
		rightMallet.transform.localPosition = Vector3.zero;
		rightMallet.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
		rightMallet.SetActive(true);
	}

	void DetachMallets()
	{
		leftMallet.transform.SetParent(null);
		leftMallet.SetActive(false);

		rightMallet.transform.SetParent(null);
		rightMallet.SetActive(false);
	}

	public void DisableVRKeyboard() 
	{
		keyboard.OnUpdate.RemoveListener(HandleUpdate);
		keyboard.OnSubmit.RemoveListener(HandleSubmit);
		keyboard.OnCancel.RemoveListener(HandleCancel);

		keyboard.Disable();

		DetachMallets();

		leftHandController.GetComponent<XRRayInteractor>().enabled = true;
		rightHandController.GetComponent<XRRayInteractor>().enabled = true;
	}

	public void HandleUpdate(string text)
	{
		keyboard.HideValidationMessage();
		inputField.text = text;
		inputField.caretPosition = inputField.text.Length;

	}
	
	public void HandleSubmit(string text)
	{
		DisableVRKeyboard();

		var eventSystem = EventSystem.current;
		if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
	}

	public void HandleCancel()
	{
		DisableVRKeyboard();

		var eventSystem = EventSystem.current;
		if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null);
	}
}
