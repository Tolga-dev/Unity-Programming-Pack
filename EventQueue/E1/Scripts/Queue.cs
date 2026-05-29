using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventQueue.E1.Scripts
{
    public class Queue : MonoBehaviour
    {
        public PopQueue firstPop;

        private QueueCommand _queueCommand;
    
        // Start is called before the first frame update
        private void Start()
        {
            _queueCommand = new QueueCommand();

            StartCommand();
        }

        private void StartCommand()
        {
            _queueCommand.Enqueue(new InstanceCmd(this));
            _queueCommand.Enqueue(new InstanceCmd(this));
            _queueCommand.Enqueue(new InstanceCmd(this));
            _queueCommand.Enqueue(new InstanceCmd(this));
            _queueCommand.ExecuteCommand();
        }
    
    
    }


    public class QueueCommand
    {
        private readonly Queue<IQCommand> _queue;
        private bool _checkRunning;
        public QueueCommand()
        {
            _queue = new Queue<IQCommand>();
            _checkRunning = false;
        }

        public void Enqueue(IQCommand qCommand)
        {
            _queue.Enqueue(qCommand);

            if (!_checkRunning)
                ExecuteCommand();

        }

        public void ExecuteCommand()
        {
            if(_queue.Count == 0)
            {
                return;
                Debug.Log("MT");            
            }
        
            IQCommand cmd = _queue.Dequeue();
            _checkRunning = true;
            cmd.Finished += OnFinishedAction;
            cmd.Execute();
        
        }
        private void OnFinishedAction()
        {
            _checkRunning = false;
            ExecuteCommand();
        }
    
    }

    public class InstanceCmd : IQCommand
    {
        public Action Finished { get; set; }
        private readonly Queue _queue;

        public InstanceCmd(Queue queue)
        {
            _queue = queue;
            Debug.Log("Created Command!");
        }
        public void Execute()
        {
        
            _queue.firstPop.OnClose += OnClose;
            Debug.Log("Execute Command!");

        }

        private void OnClose()
        {
            _queue.firstPop.OnClose -= OnClose;
            Debug.Log("Close Command!");
            Finished?.Invoke();
        }
    }

    public class PopQueue : MonoBehaviour
    {
        public Action OnClose;

        public void Close()
        {
            OnClose?.Invoke();
        }
    }
    public interface IQCommand
    {
        Action Finished { get; set; }
        void Execute();
    }
}