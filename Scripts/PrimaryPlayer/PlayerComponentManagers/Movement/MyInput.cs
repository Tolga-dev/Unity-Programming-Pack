using System;
using System.Collections;
using System.Threading.Tasks;
using PrimaryPlayer.GameEngine;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace PrimaryPlayer.PlayerComponentManagers.Movement
{
    namespace User_Defined
    {
        
        public class MyInput : MonoBehaviour
        {
             
            public static float DeadTimeSensitivity = 0.001f; // sleep 
            public static float ToTargetSpeed = 3f; // to go target, constant acceleration
            public static float Target = 1f; // max 1, we can go
            public static float Target_negatif = -1f;
            public static float Positive_Smallest = 5; 
            public static float Neg_Smallest = 0;
            public static readonly KeyCodeManager _Manager = new KeyCodeManager();
            
            
            public static void Posivite(ref float currentInput, out bool inputPressed)
            {
                if (currentInput <= 0) currentInput += Time.deltaTime * ToTargetSpeed;
                else
                {
                    if (currentInput >= Target ) currentInput = Target ;
                    else currentInput += Time.deltaTime * ToTargetSpeed;
                }
    
                inputPressed = true;
            }
    
            public static void Negative (ref float currentInput, out bool inputPressed)
            {
                if (currentInput >= 0)  currentInput -= Time.deltaTime * ToTargetSpeed;
                else
                {
                    if (currentInput <= Target_negatif ) currentInput = Target_negatif ;
                    else currentInput -= Time.deltaTime * ToTargetSpeed;
                }
    
                inputPressed = true; 
            }
    
            public static void No_Key(ref float currentInput, out bool inputPressed)
            {
                if (currentInput != 0)
                {
                    if (currentInput < 0.01*Positive_Smallest  && currentInput > Neg_Smallest ) currentInput = 0;
                    else if (currentInput > -0.01*Positive_Smallest  && currentInput < Neg_Smallest ) currentInput = 0;
    
                    if(currentInput > 0)
                        currentInput -= Time.deltaTime*ToTargetSpeed;
                    else if(currentInput < 0)
                        currentInput += Time.deltaTime*ToTargetSpeed;
                }
    
                inputPressed = false;
            }
            
        }
        

        public class MyInputHorizontal : MyInput
        {
            private static bool _inputPressed = false; // is pressed or not, return to natural        
            private static float _currentInput = 0f; // returned
            
            public static float GetAxis()
            {
                if (UnityEngine.Input.GetKey(_Manager.Keycode.Left))
                    Negative(ref _currentInput, out _inputPressed);
                if (UnityEngine.Input.GetKey(_Manager.Keycode.Right))
                    Posivite(ref _currentInput, out _inputPressed);
                if (_inputPressed == false)
                    No_Key(ref _currentInput, out _inputPressed);

                _inputPressed = false;
                return _currentInput;
            }
            
        }

        public class MyInputVertical : MyInput
        {
            private static bool _inputPressed = false; // is pressed or not, return to natural        
            private static float _currentInput = 0f; // returned
            
            public static float GetAxis()
            {
                
                if (UnityEngine.Input.GetKey(_Manager.Keycode.Forward))
                {
                    Posivite(ref _currentInput, out _inputPressed);
                }
                if (UnityEngine.Input.GetKey(_Manager.Keycode.Back))
                {
                    Negative(ref _currentInput, out _inputPressed);
                }
                if (_inputPressed == false)
                {
                    No_Key(ref _currentInput, out _inputPressed);
                }
                
                _inputPressed = false;
                return _currentInput;
            }

            
            
                     
        }
    }
}