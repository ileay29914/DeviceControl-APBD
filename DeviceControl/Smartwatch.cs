namespace DeviceControl
{
    public class Smartwatch : Device
    {
        private int _batteryPercentage;

        public int BatteryPercentage
        {
            get => _batteryPercentage;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value), "Battery percentage must be between 0 and 100.");
                _batteryPercentage = value;
            }
        }

        public Smartwatch(int id, string name, int battery) : base(id, name)
        {
            BatteryPercentage = battery;
        }

        public override void TurnOn()
        {
            if (BatteryPercentage < 11)
                throw new InvalidOperationException("Battery too low to turn on!");
            base.TurnOn();
            BatteryPercentage -= 10;
            Console.WriteLine($"{Name} is now on. Battery: {BatteryPercentage}%");
        }

        public override string ToString()
        {
            return base.ToString() + $", Battery: {BatteryPercentage}%";
        }
    }
}