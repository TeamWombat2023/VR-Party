using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pilot : MonoBehaviour
{
    public GameObject airplane;
    public GameObject rightHand;
    public TMP_Text debugText;

    private ActionBasedController rightHandXRController;

    private Quaternion startRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        //rightHandXRController = rightHand.GetComponent<ActionBasedController>();
       StartCoroutine(UpdateRotationText());
       startRotation = rightHand.transform.rotation;
       

    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log("Rotation of the right hand:"+ rightHand.transform.rotation);
       GetHandInput();
    }

    private void GetHandInput()
    {
        var currentRotation = rightHand.transform.rotation;
        //var defaultRotation = airplane.transform.forward + airplane.transform.right + airplane.transform.up;
        
        //Quaternion relative = Quaternion.Inverse(startRotation) * currentRotation;
        //difference between the plane and the right hand.
        Quaternion relative = Quaternion.Inverse(airplane.transform.rotation) * currentRotation;
        debugText.text = relative.eulerAngles.ToString();
        //Debug.Log(relative.eulerAngles.ToString());

        airplane.GetComponent<Plane>().steeringInput = relative.eulerAngles;
   
        
    }

    private void PlacePilotToPlane()
    {
    }
    
    

    public IEnumerator UpdateRotationText()
    {
        //I have relative rotation. Up for 360 degrees, down for 4-5 degrees.
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);

            var currentRotation = rightHand.transform.rotation;
            Quaternion relative = Quaternion.Inverse(startRotation) * currentRotation;
            debugText.text = " ";
            debugText.text = relative.eulerAngles.ToString();

            //airplane.GetComponent<Plane>().steeringInput = relative.eulerAngles;

            // debugText.text += "Rotation in x: " + rightHand.transform.rotation.eulerAngles.x + "\n";
            // debugText.text += "Rotation in y: " + rightHand.transform.rotation.eulerAngles.y + "\n";
            // debugText.text += "Rotation in z: " + rightHand.transform.rotation.eulerAngles.z + "\n";
        }
        
    }
    
    
}
