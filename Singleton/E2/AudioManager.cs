using UnityEngine;

namespace Singleton.E2
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        private AudioSource _audioSource;

        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AudioManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("AudioManager instance not found!");
                    }
                }
                return _instance;
            }
        }
        

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject); // Destroy duplicate instance if found
                return;
            }

            DontDestroyOnLoad(gameObject); // Persist across scenes
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip)
        {
            if (clip != null)
            {
                _audioSource.clip = clip;
                _audioSource.Play();
            }
        }
        
    }
}