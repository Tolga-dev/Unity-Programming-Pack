using System;
using UnityEngine;

namespace Observer.E1.Scripts.EventExample
{
    
    public enum JobsTypes
    {
        Builder,
        Engineer
    }
    public delegate void AssignJob();

    public class Observer2 : MonoBehaviour
    {
        private JobWorker _jobWorker;
        public static event AssignJob AssignableJob;
    
        private void Start()
        {
            AssignableJob += EngineerEvent;
            AssignableJob += BuilderEvent; 
            AssignableJob!.Invoke();
        }

        static void EngineerEvent()
        {
            Debug.Log(JobsTypes.Engineer);
        }
        static void BuilderEvent()
        {
            Debug.Log(JobsTypes.Builder);
        } 
    }

}