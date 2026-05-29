using UnityEngine;

namespace ServiceLocater.E1
{
    public interface IServiceA
    {
        void Service();
    }

    public class ServiceA : IServiceA
    {
        public void Service()
        {
            Debug.Log("Service A used!");
        }
    }
}