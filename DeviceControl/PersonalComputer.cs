namespace DeviceControl
{
    public class PersonalComputer : Device
    {
        public string? OperatingSystem { get; private set; }

        public PersonalComputer(int id, string name, string? os = null) : base(id, name)
        {
            OperatingSystem = os;
        }

        public override void TurnOn()
        {
            if (string.IsNullOrEmpty(OperatingSystem))
                throw new InvalidOperationException("Cannot turn on. No operating system installed.");
            base.TurnOn();
            Console.WriteLine($"{Name} with {OperatingSystem} is now on.");
        }

        public void InstallOS(string os)
        {
            OperatingSystem = os;
            Console.WriteLine($"{Name}: Installed {os}.");
        }

        public override string ToString()
        {
            return base.ToString() + $", OS: {OperatingSystem ?? "Not Installed"}";
        }
    }
}