using PrimaryPlayer.PlayerComponentManagers.Attack;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.EventSystems;
namespace PrimaryPlayer.Camera
{
    public class ThirdVision : MonoBehaviour
    {
        private PlayerManager.PlayerManager _playerManager;

        public GameObject headCamera;

        private float _headRotMaxVertical = 0,
            _headRotMaxHorizontal = 0;

        public float mouseSensitivity = 100f;
        private Vector3 _cameraPos;

        private RaycastHit _hitForCameraTransform;
        private UnityEngine.Camera _mainCamera;

        public float maxCameraLookUp = 20;
        public float minCameraLookUp = -20;

        private void Start()
        {
            _playerManager = GetComponent<PlayerManager.PlayerManager>();
            _mainCamera = _playerManager.mainCamera;
            _cameraPos = headCamera.transform.position - transform.position;
        }

        // Update is called once per frame
        private void Update()
        {

            // head camera set pos
            SetCameraPos();

            if (IsMouseOverUi()) return; // preventing clicking 

            SetCameraRot();
           
            
            if (_playerManager.attackManager.onSelectingTarget)
            {
                Physics.Raycast(Vector3.zero, _mainCamera.transform.forward, out _hitForCameraTransform);
               // Debug.DrawRay(transform.position, _mainCamera.transform.forward * _hitForCameraTransform.distance, Color.yellow);
                
                SetPlayerRot();
                
                Physics.Raycast(_mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f)),
                    out _playerManager.attackManager.HitForTargetPlace);
                
              //  Debug.DrawRay(_mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f)).origin,
                 //   _mainCamera.transform.forward *_playerManager.attackManager.HitForTargetPlace.distance, Color.yellow);
            }

        } 


        private float Input_Mouse_Y() => Input.GetAxis("Mouse Y") * Time.deltaTime * -mouseSensitivity;
        private float Input_Mouse_X() => Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;

        private void Lock_Cursor() => Cursor.lockState = CursorLockMode.Locked;
        private void UnLock_Cursor() => Cursor.lockState = CursorLockMode.Confined;

        private bool IsMouseOverUi()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        private void SetCameraPos()
        {
            headCamera.transform.position = transform.position + _cameraPos;
            _headRotMaxVertical += Input_Mouse_Y();
            _headRotMaxHorizontal += Input_Mouse_X();
        }

        private void SetCameraRot()
        {
            _headRotMaxVertical = Mathf.Clamp(_headRotMaxVertical, minCameraLookUp, maxCameraLookUp);
            headCamera.transform.rotation =
                Quaternion.Euler(_headRotMaxVertical, _headRotMaxHorizontal, transform.eulerAngles.z);
        }

        private void SetPlayerRot()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(new Vector3(_hitForCameraTransform.point.x, 0, _hitForCameraTransform.point.z)), 0.5f);
        }
        
    }

}