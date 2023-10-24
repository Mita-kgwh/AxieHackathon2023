using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UITemplate.Core
{
    class SoundController : MonoBehaviour
    {
        public AudioSource AuSource;


        public SoundController(AudioSource audio)
        {
            this.AuSource = audio;
        }
        public void Play()
        {
            AuSource.Play();
        }

        public void Pause()
        {
            if(AuSource.isPlaying)
                AuSource.Pause();
        }

        public void Stop()
        {
            AuSource.Stop();

        }

        public void Loop(bool value)
        {
            AuSource.loop = value;
        }

        
    }
}
