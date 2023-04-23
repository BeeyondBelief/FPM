
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Menu
{
    public class SoundMenu : MenuSection
    {
        [SerializeField] private AudioMixer _mixer;

        public static float VolumeLevel { get; private set; }
        
        public void SetVolume(float volume)
        {
            _mixer.SetFloat("Volume", volume);
            VolumeLevel = volume;
        }
    }
}
