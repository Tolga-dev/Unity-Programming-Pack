using System;
using UnityEngine;

namespace Singleton.E2
{
    public class SoundManager : MonoBehaviour
    {
        public AudioClip clip;
        
        
        public void PlayClip()
        {
            AudioManager.Instance.PlaySound(clip);
        }
        
    }
}