using UnityEngine;

namespace Singleton.E3
{
    public class Singleton<T> : MonoBehaviour where T : UnityEngine.Object
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        Debug.LogError("AudioManager instance not found!");
                    }
                }
                return instance;
            }
        }
        

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject); // Destroy duplicate instance if found
                return;
            }

            DontDestroyOnLoad(gameObject); // Persist across scenes
        }

    }
}