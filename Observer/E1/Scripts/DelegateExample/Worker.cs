using UnityEngine;

namespace Observer.E1.Scripts.DelegateExample
{
    public class Worker : IWorker
    {
        public void WorkManager(int id, Jobs type)
        {
            Debug.Log(" Work ID " + id);
        }
        
        public void DoJOb(IWorker.Work job)
        {
            job(15, Jobs.NightWatchman);
        }
    }
    
}