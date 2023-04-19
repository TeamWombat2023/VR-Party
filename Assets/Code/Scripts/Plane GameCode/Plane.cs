using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Plane : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 localVelocity;
    private Vector3 localAngularVelocity;

    private float angleOfAttack;
    private float angleOfAttackYaw;

    private Vector3 liftForce;

    private Transform respawnPoint;

    private int point = 500;


    [Header("Game Manager")]
    public PlaneGameManager planeGameManager;

    [Header("Thrust")]
    public float maxThrust;
    
    public float thrustInput;
    


    [Header("Drag")]
    public float dragScalerCoefficient;

    [Header("Lift")]
    public float liftCoefficient;


    [Header("Steering")] 
    
    //Steering input from the player
    public Vector3 steeringInput;
    
    public Vector3 turnSpeedConstant;
    public Vector3 turnAccelerationConstant;
    public AnimationCurve steeringCurve;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(SetRestartPoint());
    }


    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        CalculateLocalSpeed(dt);
        CalculateAngleOfAttack();
        
        UpdateThrust();
        UpdateDrag();
        UpdateLift();
        UpdateSteering(dt);
        
        //PrintInformation();
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
        //Debug.Log("In airplane thrustInput: "+ thrustInput);
        rb.AddRelativeForce(thrustInput * maxThrust * -1* Vector3.forward, ForceMode.Force);
    }

    private void UpdateDrag()
    {
        var lv = localVelocity;
        var lv2 = lv.sqrMagnitude;

        var dragCoefficient = Vector3.Scale(lv, new Vector3(1, 2, 2));

        var drag = -lv.normalized * (dragCoefficient.magnitude * lv2 * dragScalerCoefficient);
        //Debug.Log("Drag: "+drag);
        
        rb.AddRelativeForce(drag);
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
        var speed = Mathf.Max(0, -localVelocity.z);
        var steeringPower =  steeringCurve.Evaluate(speed);
        //Debug.Log("Steering power is: " + steeringPower);
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint took!!");
            
            planeGameManager.EnableNewCheckpoint();
        }
        else if (other.CompareTag("Powerup"))
        {
            other.gameObject.SetActive(false);
            
            planeGameManager.StartPowerupRespawnTimer();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Crashed");
            point -= 50;
            
            var respawnPosition = collision.transform.position 
                                  + new Vector3(0,500,0);

            var planeTransform = this.transform;
            planeTransform.position = respawnPosition;
            planeTransform.rotation = Quaternion.identity;

            this.rb.velocity = Vector3.zero;
            this.rb.angularVelocity = Vector3.zero;
        }
        
    }

    public IEnumerator SetRestartPoint()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            respawnPoint = this.transform;
        }
    }
}
