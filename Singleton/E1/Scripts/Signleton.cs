using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class Signleton : MonoBehaviour
{
    void Start()
    { 
        NotSafe();
    }
    private void Singleton()
    {
        threadSafetyNotLazy.Singleton instance = threadSafetyNotLazy.Singleton.Instance;
        
        instance.Init();
        threadSafetyNotLazy.Singleton instances = threadSafetyNotLazy.Singleton.Instance;
        instances.Init();
    }

    private void NotSafe()
    {
        NotSafeImplementation.Singleton instance = NotSafeImplementation.Singleton.Instance;
        instance.Init();
        NotSafeImplementation.Singleton instance2 = NotSafeImplementation.Singleton.Instance;
        instance2.Init();
        
    }
    
}

namespace LazyImplementation2 
{
    public class Singleton : MonoBehaviour
    {
        private static readonly Lazy<Singleton> _lazy =
            new Lazy<Singleton>(() => new Singleton());
        public static Singleton Instance
        {
            get { return _lazy.Value;  }
        }
        
        private Singleton() {}
    }
}
namespace LazyImplementation
{
    public sealed class Singleton
    {
        private Singleton()
        {
        }

        public static Singleton Instance { get { return Nested.instance; } }
        
        private class Nested
        { 
            [MethodImpl(MethodImplOptions.Synchronized)] // synchronized;
            static Nested()
            {
            }
            internal static readonly Singleton instance = new Singleton();
        }
    }
    
}

namespace NotSafeImplementation
{
    public sealed class Singleton : MonoBehaviour
    {
        private static Singleton _instance = null;
        private Singleton(){}

        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _instance.Init();
                Debug.Log("checked!");
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Debug.Log("destroyed!");
                Destroy(gameObject);
            }
        }

        public void Init()
        {
            Debug.Log(" success! ");
        }
    }
}


namespace threadSafetyNotLazy
{
    public class Singleton : MonoBehaviour
    {
        private static Singleton _instance = null;
        private static readonly object Lock = new object();
        Singleton(){}

        public static Singleton Instance
        {
            get
            {
                
                lock (Lock)
                {
                    var instance = GameObject.FindObjectOfType<Singleton>();
                    
                    if (instance == null)
                    {
                        GameObject obj = new GameObject(" Object Created ");

                        instance = obj.AddComponent<Singleton>();

                        DontDestroyOnLoad(obj);
                            
                    }
                }
                return _instance;
                
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _instance.Init();
                Debug.Log("checked!");
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Debug.Log("destroyed!");
                Destroy(gameObject);
            }
        }

        public void Init()
        {
            Debug.Log(" success! ");
        }
        
    }
}
 
