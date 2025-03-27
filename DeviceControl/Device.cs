namespace DeviceControl
{
    public abstract class Device : IDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }

        public Device(int id, string name)
        {
            Id = id;
            Name = name;
            IsOn = false;
        }

        public virtual void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"{Name} is now ON.");
        }

        public void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"{Name} is now OFF.");
        }

        public override string ToString()
        {
            return $"Device ID: {Id}, Name: {Name}, IsOn: {IsOn}";
        }
    }
}