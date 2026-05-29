using System;
using System.Collections;
using System.Collections.Generic;
using Player.PlayerInputLists;
using Player.PlayerStates.Movement;
using UnityEngine;
using UnityEngine.UI;


public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    
    public float horizontalInput;
    public float verticalInput;
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;
    public bool PlayerGotInTheCar = false;
    public bool PlayerTheCarOnDoor;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce = 0f;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    
    public Camera mainCamera;

    public IsPlayerHere isPlayerHere;
    public Transform PlayerParentPlayerTransform;
    public Transform CameraDynamicTransform;

    public Animator carAnimController;
    
    private void Start()
    {
        carAnimController = GetComponent<Animator>();
        isPlayerHere = gameObject.transform.Find("PlayerChecker").GetComponent<IsPlayerHere>();  
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (PlayerGotInTheCar)
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
 
            mainCamera.gameObject.transform.position = Vector3.Lerp(mainCamera.gameObject.transform.position,
                CameraDynamicTransform.transform.position, 0.1f);
        }
        

    }

    private void Update()
    {
        Debug.Log(isPlayerHere._movementManager.onDrive);
        
        if (PlayerTheCarOnDoor)
        {
            carAnimController.SetBool("DoorAnimStart", true);
            isPlayerHere._movementManager.gameObject.transform.parent = this.transform;
            isPlayerHere.InactiveInformationCanvas();
        }
        

    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
    

    void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
        
    }
    
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider,frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider,frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider,rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider,rearRightWheelTransform);
    }
    
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos,out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
    

    
}
