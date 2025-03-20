namespace DeviceControl
{
    public class EmptyBatteryException : Exception
    {
        public EmptyBatteryException(string message) : base(message) { }
    }

    public class Smartwatch : Device, IPowerNotifier
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

                if (_batteryPercentage < 20)
                    NotifyLowPower();
            }
        }

        public Smartwatch(int id, string name, int battery) : base(id, name)
        {
            BatteryPercentage = battery;
        }

        public override void TurnOn()
        {
            if (BatteryPercentage < 11)
                throw new EmptyBatteryException("Battery too low to turn on!");

            base.TurnOn(); 
            BatteryPercentage -= 10;
            Console.WriteLine($"{Name} is now on. Battery: {BatteryPercentage}%");
        }

        public void NotifyLowPower()
        {
            Console.WriteLine($"{Name}: Warning! Battery low: {BatteryPercentage}%.");
        }

        public override string ToString()
        {
            return base.ToString() + $", Battery: {BatteryPercentage}%";
        }
    }
}