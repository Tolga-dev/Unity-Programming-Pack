using UnityEngine;

namespace Singleton.E3
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            if (instance != null && instance != this)
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