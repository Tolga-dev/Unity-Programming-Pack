using System.Collections;
using UnityEngine;

namespace Service.E1.Scripts
{
    public class Service : MonoBehaviour
    {
        private AudioManager _audioManager;
        private void Start()
        {
            ConsoleAudio consoleAudio = new ConsoleAudio();

            Locator.Provide(consoleAudio);
         
        }

        private void Update()
        {
            _audioManager = Locator.GetAudioManager();

            StartCoroutine(StartLappedSong());

        }

        private IEnumerator StartLappedSong()
        {
            yield return new WaitForSeconds(2);
            _audioManager.ExecuteSound(1);
        }
    }

    public abstract class AudioManager
    {
        public abstract AudioManager _audioBehaviour();
    
        public abstract void ExecuteSound(int soundID);
    
        public abstract void StopSound(int soundID);
    
    }

    public class ConsoleAudio : AudioManager
    {
        public override AudioManager _audioBehaviour()
        {
            return this;
        }

        public override void ExecuteSound(int soundID)
        {
            return;
        }

        public override void StopSound(int soundID)
        {
            return;
        }

    }

    public class NullAudioManager : AudioManager
    {
        public override AudioManager _audioBehaviour()
        {
            return this;
        }

        public override void ExecuteSound(int soundID)
        {
        }

        public override void StopSound(int soundID)
        {
        }
    }
    public class Locator
    {
        private static AudioManager _service;
        static NullAudioManager _nullAudioManager;

        static Locator()
        {
            _nullAudioManager = new NullAudioManager();
        }

        public static void Provide(AudioManager service)
        {
            if (service == null) _service = service;
            else _service = service;
        }
        public static AudioManager GetAudioManager()
        {
            return _service;
        }
    }
}