using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Player.PlayerInputLists;
using Player.PlayerStates.Movement;
using UnityEngine;

public class IsPlayerHere : MonoBehaviour
{
    public MovementManager _movementManager;
    public CarController CarController;
    public Canvas information;
    public LayerMask layers;
    
    public List<Transform> CarTransformPoses = new List<Transform>();
    public List<Transform> CarTransformTargetPosesController = new List<Transform>() ;
    public List<Transform> CarTransformTargetPoses = new List<Transform>();
    
    
    private float Tempdistance;
    private float TempshortestPos;
    public float AveragePosPoximityPosition = 0.01f;  
    public float AverageTargetPoximityPosition = 0.01f;  
    
    private int WhichElementIsTheClosest = 0;
    private int WhichElementIsSelected = 0;
    private int IndexTargetPos = 0;
    
    private Transform NoPlayerHereCollison;
    
    private Transform getInCarPlayerCollison;
    private Transform doorLeftCollision; // child of get in car player collision
    private Transform doorRightCollision; // child of get in car player collision
    
    public Transform carGetInTransformSitDown;
    public Transform carGetOutTransformSitUp;
    private void Start()
    {
        CarController = transform.parent.GetComponent<CarController>();
        InActiveParentComponents();
        
        NoPlayerHereCollison = CarController.transform.Find("CarMaterials/Body/NoPlayerHereCollision");
        getInCarPlayerCollison = CarController.transform.Find("CarMaterials/Body/PlayerHereCollision");
        
        doorLeftCollision = getInCarPlayerCollison.transform.Find("ContainerDoorLeftCollison/DoorLeftCollision");
        doorRightCollision = getInCarPlayerCollison.transform.Find("ContainerDoorRightCollison/DoorRightCollision");
        
        NoPlayerHereCollison.gameObject.SetActive(true);
        getInCarPlayerCollison.gameObject.SetActive(false);
    }
    void Update()
    {
        if(!CarController.PlayerGotInTheCar)
            CheckPlayer(); // bindikten sonra buralar false a girmeli
        
        if (Input.GetKeyDown(Inputs.GetInTheVehicle) )
        {
            if (!_movementManager.onDrive)
            {
                ResetTransformTargetPosesValues();
                GetTargetPoses();
            }
            else
            {
                Debug.Log("Degisti");
                _movementManager.carGetInTransformSit = carGetOutTransformSitUp;
                _movementManager.playerAnimator.SetBool("OnDrive",false);
            }
        } 
        
        if (_movementManager.CanMoveInsideTheCar) 
        {
            CarController.enabled = true;
            Debug.Log("Working!");
            Vector3 moveVector = _movementManager.carGetInTransformSit.position - _movementManager.transform.position;
            _movementManager.MoveInput.x = moveVector.x; 
            _movementManager.MoveInput.z = moveVector.z;
            _movementManager.Walk();
        }
        else
        {
            
            CarController.enabled = false;
        }

        
    }

    public void CheckPlayer()
    {
 
        if (Physics.CheckSphere(transform.position, 5, layers))
        {
            
            DoActiveInformationCanvas(); 
            PlayerTransformsToGetInCar();
            
        }
        else
        {
            InactiveInformationCanvas();
        }
        
    }
    
    public void DoActiveInformationCanvas()
    {
        information.gameObject.SetActive(true);
        GetPlayerComponents();
    }

    void GetPlayerComponents()
    {
        Collider[] PlayerCollider;
        if (_movementManager == null)
        {
            PlayerCollider = Physics.OverlapSphere(transform.position, 5, layers);
            _movementManager = PlayerCollider[0].GetComponent<MovementManager>();
        }        
    }

    public void DoActiveParentComponents()
    {
        CarController.enabled = true;
    }
    public void InActiveParentComponents()
    {
        CarController.enabled = false;
    }
    public void InactiveInformationCanvas()
    {
        information.gameObject.SetActive(false);
    }
    
    public void PlayerTransformsToGetInCar()
    {

        playerFollowTargetController();

    }

    public void ResetTransformTargetPosesValues()
    {
        NoPlayerHereCollison.gameObject.SetActive(true);
        getInCarPlayerCollison.gameObject.SetActive(false);
        CarTransformTargetPosesController = new List<Transform>();
        CarTransformTargetPoses = new List<Transform>();
        
        WhichElementIsTheClosest = 0;
        WhichElementIsSelected = 0;
        IndexTargetPos = 0;
        
        if(CarTransformPoses[0] == _movementManager.gameObject.transform) { CarTransformPoses.RemoveAt(0);};
        CarTransformPoses.Insert(0,_movementManager.gameObject.transform);
        
        CarTransformTargetPosesController.AddRange(CarTransformPoses);
        TempshortestPos =  Vector3.Distance (CarTransformPoses[WhichElementIsSelected].transform.position, CarTransformTargetPosesController[WhichElementIsSelected+1].transform.position);
    }

    void playerFollowTargetController()
    {
        
        if (IndexTargetPos < CarTransformTargetPoses.Count && !CarController.PlayerTheCarOnDoor)
        {
            FollowTheTargetPositions();
            
        }
        
    }
    public bool CheckIndexBetweenTargetAndPlayerToStartAnimAndSound()
    {
        return IndexTargetPos == (CarTransformTargetPoses.Count - 1) ? true : false;
    }
    
    public void GetTargetPoses()
    {
        
        for (int i = 0; i < 3; i++)   //CarTransformPoses.Count
        {
            WhichElementIsSelected = WhichElementIsTheClosest;
            for (int j = 0; j < CarTransformTargetPosesController.Count; j++)
            {
                Tempdistance = Vector3.Distance (CarTransformPoses[WhichElementIsSelected].transform.position, CarTransformTargetPosesController[j].transform.position);
                if (TempshortestPos >= Tempdistance && CarTransformPoses[WhichElementIsSelected].transform != CarTransformTargetPosesController[j].transform)
                {
                    TempshortestPos = Tempdistance;
                    WhichElementIsTheClosest = j;
                }
                
            } 
            WhichElementIsSelected = WhichElementIsTheClosest;
            for (int k = 0; k < CarTransformPoses.Count; k++)
            {
                if (CarTransformPoses[k].transform ==
                    CarTransformTargetPosesController[WhichElementIsSelected].transform)
                {
                    CarTransformTargetPosesController.Remove(CarTransformTargetPosesController[WhichElementIsSelected]);
                    WhichElementIsSelected = k;
                    break;
                    
                }
            } 
            
            WhichElementIsTheClosest = WhichElementIsSelected;
            if (CarTransformPoses[WhichElementIsSelected].transform == CarTransformPoses[CarTransformPoses.Count - 1].transform)
            {
                CarTransformTargetPoses.Add(CarTransformPoses[WhichElementIsSelected]);
                break;
            }
            CarTransformTargetPoses.Add(CarTransformPoses[WhichElementIsSelected]);

            if (CarTransformTargetPosesController[0] == CarTransformPoses[0])
            {
                CarTransformTargetPosesController.Remove(CarTransformTargetPosesController[0]);
            }
            
            TempshortestPos =  Vector3.Distance (CarTransformPoses[WhichElementIsSelected].transform.position, CarTransformTargetPosesController[0].transform.position);
            
        }
    }

    public void FollowTheTargetPositions()
    {
        if (!_movementManager.IsThereInput()) 
        {
            _movementManager.playerAnimator.SetBool("GoToCar", true);
           if (ReturnDistanceCarTransformPosesAndPlayer(IndexTargetPos))
           {
               IndexTargetPos++;
           }
           else
           {
               Vector3 moveVector = CarTransformTargetPoses[IndexTargetPos].transform.position - _movementManager.transform.position;
                   
               _movementManager.MoveInput.x = moveVector.x; 
               _movementManager.MoveInput.z = moveVector.z; 
               _movementManager.transform.rotation = Quaternion.LookRotation(_movementManager.MoveInput);
               _movementManager.Walk();
               
               if (ReturnDistanceCarTransformTargetAndPlayer(IndexTargetPos) && CheckIndexBetweenTargetAndPlayerToStartAnimAndSound()) // distance olayi problemli anladim problemi yarin belki baska gun
               {
                    
                   _movementManager.MoveInput = Vector3.zero;
                   _movementManager.carGetInTransformSit = carGetInTransformSitDown;
                   
                   CarController.PlayerTheCarOnDoor = true; 
                   
                   SetActives();
                   CarController.carAnimController.SetBool("DoorAnimStart", true);
                   _movementManager.playerAnimator.SetBool("StartCarLayer",true);
                   
                   Debug.Log("Got in the Car! Start Anim and change Pos!");
                   
               }

           }
           
        }
        else
        {
            _movementManager.playerAnimator.SetBool("GoToCar", false);
            
            ResetTransformTargetPosesValues();
            DoActiveInformationCanvas();
            
            

        }

    }

    public void SetActives()
    {
        NoPlayerHereCollison.gameObject.SetActive(!NoPlayerHereCollison.gameObject.activeSelf);
        getInCarPlayerCollison.gameObject.SetActive(!getInCarPlayerCollison.gameObject.activeSelf);
    }
    public bool ReturnDistanceCarTransformPosesAndPlayer(int index)
    {
        return (Vector3.Distance(CarTransformTargetPoses[index].transform.position, _movementManager.transform.position) <= AveragePosPoximityPosition);
    }
    public bool ReturnDistanceCarTransformTargetAndPlayer(int index)
    {
//        Debug.Log(Vector3.Distance(CarTransformTargetPoses[index].transform.position, _movementManager.transform.position));
        return (Vector3.Distance(CarTransformTargetPoses[index].transform.position, _movementManager.transform.position) <= AverageTargetPoximityPosition);
    }
}
