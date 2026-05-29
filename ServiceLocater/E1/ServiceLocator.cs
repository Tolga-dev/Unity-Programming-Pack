using System;
using System.Collections.Generic;
using ServiceLocater.E1;
using UnityEngine;
using Object = Update.E1.Object;

namespace ServiceLocater.E1
{
    public class ServiceLocator : MonoBehaviour
    {
        private void Start()
        {
            ServiceController.Instance.Register<IServiceA>(new ServiceA());
            ServiceController.Instance.Register<IServiceB>(new ServiceB());

            var client = new Client
            {
                ServiceA = ServiceController.Instance.GetService<IServiceA>(),
                ServiceB = ServiceController.Instance.GetService<IServiceB>()
            };
            
            client.Job();
            

        }
    }

    public class ServiceController
    {
        private Dictionary<Type, object> _registry = new Dictionary<Type, object>();
        private static ServiceController _locator;

        public static ServiceController Instance
        {
            get
            {
                if (_locator == null)
                    _locator = new ServiceController();
                return _locator;
            }
        }

        public void Register<T>(T serviceInstance)
        {
            _registry[typeof(T)] = serviceInstance;
        }

        public T GetService<T>()
        {
            T serviceInstance = (T)_registry[typeof(T)];
            return serviceInstance;
        }

    }
    public class Client
    {
        public IServiceA ServiceA; 
        public IServiceB ServiceB;

        public void Job()
        {
            ServiceA!.Service();
            ServiceB!.Service();
        }
        
    }


}
