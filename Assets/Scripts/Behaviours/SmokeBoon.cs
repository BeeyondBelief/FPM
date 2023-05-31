namespace Behaviours
{
    public class SmokeBoon: Boon
    { 
        public float CurrentStealthPower { get; }
        public override string CharImage => "S";
        
        public SmokeBoon(float stealthPower)
        {
            CurrentStealthPower = stealthPower;
        }
    }
}