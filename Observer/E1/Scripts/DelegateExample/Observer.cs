using UnityEngine;

namespace Observer.E1.Scripts.DelegateExample
{
    
    public class Observer : MonoBehaviour
    {
        private Worker _worker;

        private void Start()
        {
            _worker = new Worker();
            IWorker.Work workType = new IWorker.Work(_worker.WorkManager);
            _worker.DoJOb(workType);
            
        }
        
        
    }
}