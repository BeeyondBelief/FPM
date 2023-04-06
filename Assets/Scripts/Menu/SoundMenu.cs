
namespace Menu
{
    public class SoundMenu : MenuSection
    {
        public static float VolumeLevel { get; private set; }
        
        public void SetVolume(float volume)
        {
            VolumeLevel = volume;
        }
    }
}
