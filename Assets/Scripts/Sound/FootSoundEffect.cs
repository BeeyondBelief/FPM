using UnityEngine;

namespace Sound
{
    public class FootSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _soundEffects;

        public AudioClip GetSound()
        {
            return _soundEffects[Random.Range(0, _soundEffects.Length - 1)];
        }
    }
}
