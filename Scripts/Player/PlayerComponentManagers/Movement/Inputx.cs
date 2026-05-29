using System;
using System.Collections;
using PrimaryPlayer.GameEngine;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace PrimaryPlayer.PlayerComponentManagers.Movement
{
    public class _Input_h
    {        // in this section will be used for get axis

        /*
        private static float DeadTimeSensitivity = 0.001f; // sleep 
        
        private static float ToTargetSpeed = 3f; // to go target, constant acceleration
        
        private static float Target_x = 1f; // max 1, we can go
        private static float Target_negatif_x = -1f;

        private static bool InputPressed = false; // is pressed or not, return to natural        
     
        private static float CurrentInput = 0f; // returned
        
        private static float Positive_Smallest_x = 5; 
        private static float Neg_Smallest_x = 0;
        
        private static readonly MovementCommandManager Manager = new MovementCommandManager();

        public float Dummy()    
        {
            return 0f;
        }
        public static float GetAxis()
        {
            
            if (Input.GetKey(Manager.Keycode.Left))
                Negative_x();
            if (Input.GetKey(Manager.Keycode.Right))
                Posivite_x();
            if (InputPressed == false)
                No_Key();
            
            InputPressed = false;
            return CurrentInput;
        }

        private static float Posivite_x()
        {
            
            if (CurrentInput <= 0) CurrentInput += Time.deltaTime * ToTargetSpeed;
            else
            {
                if (CurrentInput >= Target_x) CurrentInput = Target_x;
                else CurrentInput += Time.deltaTime * ToTargetSpeed;
            }

            InputPressed = true;
            return CurrentInput;
        }

        private static float Negative_x()
        {
            if (CurrentInput >= 0)  CurrentInput -= Time.deltaTime * ToTargetSpeed;
            else
            {
                if (CurrentInput <= Target_negatif_x) CurrentInput = Target_negatif_x;
                else CurrentInput -= Time.deltaTime * ToTargetSpeed;
            }

            InputPressed = true;
            return CurrentInput;
        }

        private static float No_Key()
        {
            if (CurrentInput != 0)
            {
                if (CurrentInput < 0.01*Positive_Smallest_x && CurrentInput > Neg_Smallest_x) CurrentInput = 0;
                else if (CurrentInput > -0.01*Positive_Smallest_x && CurrentInput < Neg_Smallest_x) CurrentInput = 0;

                if(CurrentInput > 0)
                    CurrentInput -= Time.deltaTime*ToTargetSpeed;
                else if(CurrentInput < 0)
                    CurrentInput += Time.deltaTime*ToTargetSpeed;
            }

            InputPressed = false;
            return CurrentInput;
        }
        public static IEnumerator SleepForDead()
        {
            yield return new WaitForSeconds(DeadTimeSensitivity);
        }
        */

    }
}