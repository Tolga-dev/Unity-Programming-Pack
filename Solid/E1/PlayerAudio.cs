using UnityEngine;

namespace Solid.E1
{
    
    public class PlayerAudio : MonoBehaviour
    {
        private AudioSource _playerSound;

        private void Start()
        {
            _playerSound = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            //_playerSound.Play();
            Debug.Log("Play Sound Player");
        }
        
    }
}