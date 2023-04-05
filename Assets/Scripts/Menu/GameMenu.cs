

namespace Menu
{
    public class GameMenu : MenuSection
    {
        public static bool ToggleButtonsEnabled { get; private set; }

        public void SetToggleButtonBehavior(bool useToggle)
        {
            ToggleButtonsEnabled = useToggle;
        }
    }
}
