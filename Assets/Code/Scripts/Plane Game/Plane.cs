using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 localVelocity;
    private Vector3 localAngularVelocity;

    private float angleOfAttack;
    private float angleOfAttackYaw;

    private Vector3 liftForce;

    [Header("Thrust")]
    public float thrust;
    public float maxThrust;

    [Header("Lift")]
    public float liftCoefficient;


    [Header("Steering")] 
    
    //Steering input from the player
    public Vector3 steeringInput;
    
    
    public Vector3 turnSpeedConstant;
    public Vector3 turnAccelerationConstant;
    
    //Steering power normally determines with sigmoid(speed)
    public float steeringPower;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        CalculateLocalSpeed(dt);
        CalculateAngleOfAttack();
        
        UpdateThrust();
        UpdateLift();
        UpdateSteering(dt);
        
        PrintInformation();
    }

    private void CalculateLocalSpeed(float dt)
    {
        var invRotation = Quaternion.Inverse(rb.rotation);
        velocity = rb.velocity;
        localVelocity = invRotation * velocity;
        localAngularVelocity = invRotation * rb.angularVelocity;



    }

    private void CalculateAngleOfAttack()
    {
        if (localVelocity.sqrMagnitude < 0.1f)
        {
            angleOfAttack = 0;
            angleOfAttackYaw = 0;
            return;
        }

        angleOfAttack = Mathf.Atan2(-localVelocity.y, localVelocity.z);
        angleOfAttackYaw = Mathf.Atan2(-localVelocity.x, localVelocity.z);
    }


    private void PrintInformation()
    {
        Debug.Log("Velocity: "+ velocity);
        Debug.Log("Local Velocity: "+ localVelocity);
        Debug.Log("Local Angular Velocity: "+ localAngularVelocity);
        Debug.Log("----------");
        
        Debug.Log("AoA: "+angleOfAttack);
        Debug.Log("AoA Yaw: "+angleOfAttackYaw);
        Debug.Log("----------");
        
        Debug.Log("Lift: "+liftForce);

        Debug.Log("**************************************");
    }

    private void UpdateThrust()
    {
        rb.AddRelativeForce(thrust * maxThrust * Vector3.forward, ForceMode.Force);
    }

    private void UpdateDrag()
    {
        
        
    }

    private void UpdateLift()
    {
        if (localVelocity.sqrMagnitude < 1f) return;

        var v2 = localVelocity.sqrMagnitude;
        var liftDirection = angleOfAttack < 0 ? -1 : 1;

        if (liftDirection == 1)
        {
            liftForce = (liftDirection * liftCoefficient) * v2 * Vector3.up;
        }
        else if (liftDirection == -1)
        { 
            liftForce = (liftDirection * liftCoefficient) * v2 * Vector3.down;
        }

        rb.AddRelativeForce(liftForce);

    }

    private void UpdateSteering(float dt)
    {
        var targetAngularVelocity = Vector3.Scale(steeringInput, turnSpeedConstant * steeringPower);

        var currentAngularVelocity = localAngularVelocity * Mathf.Rad2Deg;

        var requiredVelocityChange = new Vector3(
            CalculateRequiredSteering(dt, currentAngularVelocity.x, targetAngularVelocity.x,
                turnAccelerationConstant.x * steeringPower),
            CalculateRequiredSteering(dt, currentAngularVelocity.y, targetAngularVelocity.y,
                turnAccelerationConstant.y * steeringPower),
            CalculateRequiredSteering(dt, currentAngularVelocity.z, targetAngularVelocity.z,
                turnAccelerationConstant.z * steeringPower)
        );

        rb.AddRelativeTorque(requiredVelocityChange* Mathf.Deg2Rad,ForceMode.VelocityChange);
    }

    


    private float CalculateRequiredSteering(float dt, float currentAngularVelocity, float targetAngularVelocity,
        float acceleration)
    {
        var diff = targetAngularVelocity - currentAngularVelocity;
        var currentAcceleration = acceleration * dt;

        return Mathf.Clamp(diff, -currentAcceleration, currentAcceleration);
    }

    
}
