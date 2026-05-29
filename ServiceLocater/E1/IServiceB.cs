using UnityEngine;

namespace ServiceLocater.E1
{
    public interface IServiceB
    {
        void Service();
    }

    public class ServiceB : IServiceB
    {
        public void Service()
        {
            Debug.Log("Service B used!");
        }
    }
}