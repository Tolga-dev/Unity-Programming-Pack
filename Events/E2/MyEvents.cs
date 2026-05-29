using System;
using UnityEngine;

namespace Events.E2
{
    public class MyEvents : MonoBehaviour
    {

        public delegate void Notify();
        public event Notify ProcessCompleted;

        public event EventHandler EventHandlerProcessCompleted;
        
        public event EventHandler<bool> EventHandlerFoo;
        
        public event EventHandler<ProcessEvent> EventHandlerProcess;
        
        private void Start()
        {

            ProcessCompleted += ProcessCompletedNotification;
            ProcessCompleted += ProcessCompletedSound;
            ProcessCompleted += ProcessCompletedUI;
            

            EventHandlerProcessCompleted += EventHandlerProcessCompletedNotification;
            EventHandlerProcessCompleted += EventHandlerProcessCompletedUI;

            EventHandlerFoo += EventHandlerFooCompleted;

            EventHandlerProcess += EventHandlerProcessEvent;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ProcessCompleted!.Invoke();
                EventHandlerProcessCompleted!.Invoke(this, EventArgs.Empty);
                EventHandlerFoo!.Invoke(this, true);
                CallProcessEvent();
            }
        }

        private void CallProcessEvent()
        {
            var data = new ProcessEvent
            {
                IsFinish = true
            };
            EventHandlerProcess!.Invoke(this, data);
        }

        private void ProcessCompletedNotification()
        {
            Debug.Log("Process Completed Message");
        }
        private void ProcessCompletedSound()
        {
            Debug.Log("Process Completed Sound");
        }
        private void ProcessCompletedUI()
        {
            Debug.Log("Process Completed UI");
        }

        private void EventHandlerProcessCompletedNotification(object sender, EventArgs e)
        {
            Debug.Log("Event Handler Notification Process Completed");
        }
        private void EventHandlerProcessCompletedUI(object sender, EventArgs e)
        {
            Debug.Log("Event Handler UI Process Completed");
        }
        
        private void EventHandlerFooCompleted(object sender, bool e)
        {
            Debug.Log(e);
        }

        private void EventHandlerProcessEvent(object sender, ProcessEvent eProcessEvent)
        {
            Debug.Log(eProcessEvent.IsFinish);
        }
        
    }

    public class ProcessEvent : EventArgs
    {
        public bool IsFinish { get; set; }
    }
    
}
