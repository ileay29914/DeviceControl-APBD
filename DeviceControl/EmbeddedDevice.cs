using System.Text.RegularExpressions;

namespace DeviceControl
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message) { }
    }

    public class EmbeddedDevice : Device
    {
        private static readonly Regex IpRegex = new(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");

        private string _ipAddress;
        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                if (!IpRegex.IsMatch(value))
                    throw new ArgumentException("Invalid IP address format.");

                _ipAddress = value;
            }
        }

        public string NetworkName { get; private set; }

        public EmbeddedDevice(int id, string name, string ip, string network) : base(id, name)
        {
            IpAddress = ip;
            NetworkName = network;
        }

        public void Connect()
        {
            if (!NetworkName.Contains("MD Ltd."))
                throw new ConnectionException("Cannot connect. Invalid network name.");

            Console.WriteLine($"{Name} connected to {NetworkName}.");
        }

        public override void TurnOn()
        {
            Connect();
            base.TurnOn(); 
            Console.WriteLine($"{Name} is now on.");
        }

        public override string ToString()
        {
            return base.ToString() + $", IP: {IpAddress}, Network: {NetworkName}";
        }
    }
}