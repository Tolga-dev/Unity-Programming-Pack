using UnityEngine;

namespace Singleton.E3
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