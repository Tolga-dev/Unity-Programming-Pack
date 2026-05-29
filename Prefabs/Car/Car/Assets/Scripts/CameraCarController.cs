using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCarController : MonoBehaviour
{
    public GameObject headCamera;
    private float headRotMaxVertical = 0, headRotMaxHorizontal = 0;
    
    public float mouseSensitivity = 100f;
    private Vector3 CameraPos;

    private RaycastHit hitForCamera;
 
    // Start is called before the first frame update
    void Start()
    { 
        
        CameraPos = headCamera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        headCamera.transform.position = transform.position + CameraPos;
        
        headRotMaxVertical += Input.GetAxis("Mouse Y") * Time.deltaTime * -mouseSensitivity;
        headRotMaxHorizontal += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        
        headRotMaxVertical = Mathf.Clamp(headRotMaxVertical,-20, 20); 
        headCamera.transform.rotation = Quaternion.Euler(headRotMaxVertical,headRotMaxHorizontal,transform.eulerAngles.z);
        

    }
    
    
}
