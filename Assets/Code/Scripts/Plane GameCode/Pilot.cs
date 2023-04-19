using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pilot : MonoBehaviour
{
    public Plane airplane;
    public GameObject rightHand;
    public GameObject leftHand;
    public TMP_Text debugText;


    private bool isSteeringEnabled;
    public AnimationCurve steeringXCurve;
    public AnimationCurve steeringZCurve;
    
    private bool isSpeedAdjustmentEnabled;
    private float leftHandPositionBeginning;
    public AnimationCurve thrustMappingCurve;

    

    // Start is called before the first frame update
    void Start()
    {
        //rightHandXRController = rightHand.GetComponent<ActionBasedController>();
       isSteeringEnabled = false;

    }

    void FixedUpdate()
    {
       //Debug.Log("Rotation of the right hand:"+ rightHand.transform.rotation);
       if (isSteeringEnabled)
       {
           GetHandInput();
       }
       else
       {
           SendStabilizationSignalToPlane();
       }

       if (isSpeedAdjustmentEnabled)
       {
           SendThrustDifferenceToPlane();
       }
       
    }
    public void EnableSteering()
    {
        isSteeringEnabled = true;
        debugText.text = "Grabbed";
        
        //todo steering anında elin kitlenmesi durumu yapilacak.
        
    }

    public void DisableSteering()
    {
        isSteeringEnabled = false;
        debugText.text = "Left";
    }


    public void EnableThrustModification()
    {
        isSpeedAdjustmentEnabled = true;
        debugText.text = "thrust modification begun";
        leftHandPositionBeginning = leftHand.transform.localPosition.z;
    }
    
    
    public void DisableThrustModification()
    {
        isSpeedAdjustmentEnabled = false;
        debugText.text = "thrust modification end";
    }

    private void GetHandInput()
    {
        var currentHandRotation = rightHand.transform.rotation;
        //difference between the plane and the right hand.
        Quaternion relativeQuat = Quaternion.Inverse(airplane.transform.rotation) * currentHandRotation;
        float relativeVecX = relativeQuat.eulerAngles.x < 180 ? relativeQuat.eulerAngles.x : relativeQuat.eulerAngles.x -360;
        float relativeVecY = relativeQuat.eulerAngles.y < 180 ? relativeQuat.eulerAngles.y : relativeQuat.eulerAngles.y - 360;
        float relativeVecZ = relativeQuat.eulerAngles.z < 180 ? relativeQuat.eulerAngles.z : relativeQuat.eulerAngles.z - 360;


        relativeVecX = steeringXCurve.Evaluate(relativeVecX);
        relativeVecZ = steeringZCurve.Evaluate(relativeVecZ);


        relativeVecX = -relativeVecX;
        relativeVecZ = -relativeVecZ;
        Vector3 relativeVec = new Vector3(relativeVecX, relativeVecY, relativeVecZ);

        //Quaternion absoluteHandRotation = Quaternion.Inverse(relative) * currentRotation;
        
        debugText.text = "Hand's rotation: " + currentHandRotation.eulerAngles.ToString() + "\n";
        debugText.text += "RelativeVec:" + relativeVec + "\n";

        airplane.steeringInput = relativeVec;
    }

    private void SendThrustDifferenceToPlane()
    {
        var currentLeftHandPosition = leftHand.transform.localPosition.z;

        //var currentThrust = airplane.GetComponent<Plane>().thrust;

        var difference = currentLeftHandPosition - leftHandPositionBeginning;

        leftHandPositionBeginning = currentLeftHandPosition;

        var newThrust = airplane.thrustInput + thrustMappingCurve.Evaluate(difference);
        if (newThrust > 1)
        {
            newThrust = 1;
        }
        else if (newThrust < 0)
        {
            newThrust = 0;
        }
        
        airplane.thrustInput = newThrust;
    }


    private void SendStabilizationSignalToPlane()
    {
        var stabilizedPosition = new Vector3(0,0,0);
        airplane.steeringInput = stabilizedPosition;
    }



    
    
    public IEnumerator UpdateRotationText()
    {
        //I have relative rotation. Up for 360 degrees, down for 4-5 degrees.
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);

            var currentRotation = rightHand.transform.rotation;
            debugText.text = " ";

            //airplane.GetComponent<Plane>().steeringInput = relative.eulerAngles;

            // debugText.text += "Rotation in x: " + rightHand.transform.rotation.eulerAngles.x + "\n";
            // debugText.text += "Rotation in y: " + rightHand.transform.rotation.eulerAngles.y + "\n";
            // debugText.text += "Rotation in z: " + rightHand.transform.rotation.eulerAngles.z + "\n";
        }
        
    }
    
    
}
