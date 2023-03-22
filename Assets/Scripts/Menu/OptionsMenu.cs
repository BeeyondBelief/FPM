using UnityEngine;

namespace Menu
{
    public class OptionsMenu: MonoBehaviour
    {
        public static float VolumeLevel { get; private set; }
        public static bool ToggleButtonsEnabled { get; private set; }
        
        public void SetVolume(float volume)
        {
            VolumeLevel = volume;
        }

        public void SetToggleButtonBehavior(bool useToggle)
        {
            ToggleButtonsEnabled = useToggle;
        }
    }
}