using System.Collections;
using System.Collections.Generic;
using Player.Params;
using Player.PlayerInputLists;
using Player.PlayerStates.Attack;
using Player.PlayerStates.Movement;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject headCamera;
    private MovementManager _movementManager;
    private AttackManager _attackManager;
    private PlayerAttackParams _attackParams;
    private float headRotMaxVertical = 0, headRotMaxHorizontal = 0;
    
    public float mouseSensitivity = 100f;
    private Vector3 CameraPos;

    private RaycastHit hitForCamera;
 
    // Start is called before the first frame update
    void Start()
    { 
        _movementManager = GetComponent<MovementManager>();
        _attackManager = GetComponent<AttackManager>();
        _attackParams = _attackManager.data;
         
       // Cursor.lockState = CursorLockMode.Locked;
        CameraPos = headCamera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        headCamera.transform.position = transform.position + CameraPos;
        
        headRotMaxVertical += Input.GetAxis("Mouse Y") * Time.deltaTime * -mouseSensitivity;
        headRotMaxHorizontal += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        
        headRotMaxVertical = Mathf.Clamp(headRotMaxVertical, _attackParams.minCameraLookUp, _attackParams.maxCameraLookUp); 
        headCamera.transform.rotation = Quaternion.Euler(headRotMaxVertical,headRotMaxHorizontal,transform.eulerAngles.z);

        if (_movementManager.MoveInput != Vector3.zero || _attackManager.OnSelectingTarget)
        {
            Physics.Raycast(Vector3.zero, headCamera.transform.GetChild(0).forward, out hitForCamera);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(hitForCamera.point.x,0,hitForCamera.point.z)),0.5f);        Debug.DrawLine(Vector3.zero, hitForCamera.point);
          //  Debug.Log(hitForCamera.point);
            Debug.DrawLine(Vector3.zero, hitForCamera.point);
            //playerBody.Rotate(Vector3.up * mouseX);

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            Physics.Raycast(ray, out _attackManager.hitForCameraAtes);
            Debug.DrawLine(ray.origin, _attackManager.hitForCameraAtes.point);
        }

        if (_movementManager.isOpenedInventory)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        
    }
    
}
