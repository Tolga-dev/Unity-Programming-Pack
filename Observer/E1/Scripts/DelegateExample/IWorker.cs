using UnityEngine;

namespace Observer.E1.Scripts.DelegateExample
{
    public enum Jobs
    {
        NightWatchman,
        Laborer
    }

    public interface IWorker  
    {
        public delegate void Work(int id, Jobs type);

    }

}