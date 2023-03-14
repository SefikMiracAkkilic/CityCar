using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControler : MonoBehaviour

{
    private const string horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private float breakForce;
    private float currentBreakForce;

    [SerializeField] private float motorForce;
    private bool isBreaking;
    [SerializeField] private float maxSteerAngle;
     private float steerAngle;
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;




    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0;
        ApplyBreaking();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
        getInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        

    }
    private void getInput()
    {
        horizontalInput = Input.GetAxis(horizontal);
        verticalInput = Input.GetAxis(Vertical);
        isBreaking = Input.GetKey(KeyCode.Space);

    }
    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }
    private void HandleSteering()
    {
        steerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
        
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);

    }
    private void UpdateSingleWheel(WheelCollider wheelCollider,Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos; 
        wheelTransform.rotation = rot;
    }

}
